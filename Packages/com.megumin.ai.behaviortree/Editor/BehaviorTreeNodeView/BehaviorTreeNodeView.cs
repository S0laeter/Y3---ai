﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using System;
using UnityEditor;
using System.ComponentModel;
using Megumin.AI.Editor;
using System.Linq;
using System.Reflection;

namespace Megumin.AI.BehaviorTree.Editor
{
    public partial class BehaviorTreeNodeView : Node
    {

        public new class UxmlFactory : UxmlFactory<BehaviorTreeNodeView, UxmlTraits> { }

        /// <summary>
        /// 没办法，基类只接受路径。
        /// </summary>
        public BehaviorTreeNodeView()
            : base(AssetDatabase.GetAssetPath(Resources.Load<VisualTreeAsset>("BehaviorTreeNodeView")))
        {
            UseDefaultStyling();
            StyleSheet styleSheet = Resources.Load<StyleSheet>("BehaviorTreeNodeView");
            styleSheets.Add(styleSheet);
            this.AddToClassList("behaviorTreeNode");

            Body = this.Q("body");
            Description = this.Q<Label>("description");
            ShortGUID = this.Q<Label>("guid");
            Icon = this.Q<Button>("icon", "treeElementIcon");
            DynamicMarker = this.Q<Label>("dynamicMarker");

            DetailContainer = this.Q("detailContainer");
            Detail = this.Q<Label>("detail");

            BodyExpendContainer = Body.Q("expendContainer");
            ContentExpendContainer = this.Q("contents").Q("expendContainer");

            ToprightBadgeContainer = this.Q("toprightBadgeContainer");
            AbortTypeButton = ToprightBadgeContainer.Q<Button>("abortType", "abortType");

            Index = this.Q<Label>("nodeIndex");
            decoratorContainer = this.Q<VisualElement>("decorator");
            //decoratorContainer.AddManipulator(new TestMouseManipulator());
            //为了屏蔽 装饰器内 框选误选中node。 组织事件向父级传播。
            //TODO，不知道为啥要同时停止MouseDownEvent  MouseUpEvent 才有效。
            decoratorContainer.AddManipulator(new StopPropagationMouseManipulator<MouseDownEvent, MouseUpEvent>());
            //decoratorContainer.RegisterCallback<MouseUpEvent>(evt =>
            //{
            //    this.LogMethodName("MouseUpEvent WillStop");
            //    evt.StopImmediatePropagation();
            //}, TrickleDown.NoTrickleDown);

            //decoratorContainer.RegisterCallback<MouseDownEvent>(evt =>
            //{
            //    this.LogMethodName("MouseDownEvent WillStop");
            //    //evt.StopPropagation();
            //    evt.StopImmediatePropagation();
            //}, TrickleDown.NoTrickleDown);

            DecoretorListView = decoratorContainer.Q<ListView>();
            //DecoretorListView.reorderable = true;
            //DecoretorListView.reorderMode = ListViewReorderMode.Animated;
            DecoretorListView.makeItem += ListViewMakeDecoratorView;
            DecoretorListView.bindItem += ListViewBindDecorator;

#if UNITY_2022_3_OR_NEWER
            DecoretorListView.itemsChosen += DecoretorListView_onItemsChosen;
#else
            DecoretorListView.onItemsChosen += DecoretorListView_onItemsChosen;
#endif

            DecoretorListView.itemIndexChanged += DecoretorListView_itemIndexChanged;
            DecoretorListView.destroyItem += DecoretorListView_destroyItem;

            //设置为仅垂直滚动
            DecoretorListView.horizontalScrollingEnabled = false; //不生效

            var scrollView = DecoretorListView.Q<ScrollView>();
            scrollView.mode = ScrollViewMode.Vertical;
            scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
            scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
            scrollView.contentViewport.style.overflow = Overflow.Visible;
            scrollView.contentContainer.style.overflow = Overflow.Visible;

            this.RegisterCallback<TooltipEvent>(OnToolTip);
            RegisterCallback<KeyDownEvent>(OnKeyDownEvent, TrickleDown.NoTrickleDown);
        }

        public NodeWrapper SONode;

        public BehaviorTreeView TreeView { get; internal set; }

        public Port InputPort { get; private set; }
        public Port OutputPort { get; private set; }
        public VisualElement Body { get; private set; }
        public Label Description { get; private set; }
        public Label ShortGUID { get; private set; }
        public Button Icon { get; private set; }
        public Label DynamicMarker { get; private set; }
        public VisualElement DetailContainer { get; private set; }
        public VisualElement BodyExpendContainer { get; private set; }
        public VisualElement ContentExpendContainer { get; private set; }
        public Label Detail { get; private set; }
        public VisualElement ToprightBadgeContainer { get; private set; }
        public Button AbortTypeButton { get; private set; }
        public Label Index { get; private set; }
        public VisualElement decoratorContainer { get; }
        public ListView DecoretorListView { get; }
        public BTNode Node { get; private set; }
        /// <summary>
        /// 是不是仅用于测试的假节点
        /// </summary>
        public bool IsFakeNode { get; private set; }

        public override void OnSelected()
        {
            //this.LogMethodName(title);
            base.OnSelected();
            if (SONode)
            {
                Selection.activeObject = SONode;
                SetNodeScript();
            }
        }

        public override void OnUnselected()
        {
            //this.LogMethodName(title);
            base.OnUnselected();

            //取消选中时保留显示

            //this.LogMethodName(title);
            //if (Selection.activeObject == SONode)
            //{
            //    Selection.activeObject = null;
            //}
        }

        public override void SetPosition(Rect newPos)
        {
            //对位置取整，不然保存的时候会有小数。 会导致拖拽时抖动，放弃。
            //newPos = new Rect((int)newPos.x, (int)newPos.y, newPos.width,newPos.height);

            base.SetPosition(newPos);
            if (SONode.Node.Meta == null)
            {
                SONode.Node.Meta = new NodeMeta();
            }

            //this.LogFuncName();
            TreeView.UndoRecord($"SetPosition    [{SONode.Node.GetType().Name}]");
            SONode.Node.Meta.x = newPos.x;
            SONode.Node.Meta.y = newPos.y;

            //父节点重新排序
            foreach (var edge in InputPort.connections)
            {
                if (edge.output.node is BehaviorTreeNodeView nodeView
                    && nodeView.SONode?.Node is BTParentNode parentNode)
                {
                    TreeView.SortChild(parentNode);
                }
            }

            TreeView?.UpdateNodeIndex();
        }


        public NodeWrapper CreateSOWrapperIfNull(BTNode node, bool forceRecreate = false)
        {
            var soWrapper = SONode;
            if (!soWrapper)
            {
                if (TreeView.NodeWrapperCache.TryGetValue(node.GUID, out var cacheWrapper))
                {
                    //创建新的SO对象在 Inpector锁定显示某个节点时，会出现无法更新的问题。
                    //尝试复用旧的SOWrapper

                    //Debug.Log("尝试复用旧的SOWrapper");
                    soWrapper = cacheWrapper;
                }
            }

            if (!soWrapper || forceRecreate)
            {
                soWrapper = this.CreateSOWrapper<NodeWrapper>();
                TreeView.NodeWrapperCache[node.GUID] = soWrapper;
            }
            return soWrapper;
        }

        public const string StartNodeClass = "startNode";

        internal void SetNode(BTNode node, bool fakeNode = false)
        {
            viewDataKey = node?.GUID;
            IsFakeNode = fakeNode;
            Node = node;
            ReloadView();
        }

        public void CreatePort(BTNode node)
        {
            if (InputPort == null)
            {
                InputPort = new BehaviorTreePortView(Direction.Input, Port.Capacity.Single);
                inputContainer.Add(InputPort);

                if (node is IInputPortInfoy<string> infoy)
                {
                    string inputPortInfo = infoy.InputPortInfo;
                    InputPort.portName = inputPortInfo;
                    InputPort.tooltip = inputPortInfo;
                    InputPort.SetToClassList(UssClassConst.portInfo, !string.IsNullOrEmpty(inputPortInfo));
                    InputPort.SetToClassList(UssClassConst.inputPortName, !string.IsNullOrEmpty(inputPortInfo));
                }
            }

            if (OutputPort == null)
            {
                Port.Capacity multiOutputPort = node is OneChildNode ? Port.Capacity.Single : Port.Capacity.Multi;
                OutputPort = new BehaviorTreePortView(Direction.Output, multiOutputPort);
                outputContainer.Add(OutputPort);

                if (node is IOutputPortInfoy<string> infoy)
                {
                    string outputPortInfo = infoy.OutputPortInfo;
                    OutputPort.portName = outputPortInfo;
                    OutputPort.tooltip = outputPortInfo;
                    OutputPort.SetToClassList(UssClassConst.portInfo, !string.IsNullOrEmpty(outputPortInfo));
                    OutputPort.SetToClassList(UssClassConst.outputPortName, !string.IsNullOrEmpty(outputPortInfo));
                }
            }

            outputContainer.SetToClassList("unDisplay", node is BTActionNode);
        }

        /// <summary>
        /// 因为UndoRedo时内存实例对象会改变，所以每次通过guid取得新的实例。
        /// </summary>
        /// <param name="forceReCreateSoWrapper"></param>
        public virtual void ReloadView(bool forceReCreateSoWrapper = false)
        {
            //清除旧的class typeName
            RemoveFromClassList(Node?.GetType().Name ?? "NullNode");

            var node = Node;
            if (IsFakeNode)
            {
                //测试用NodeView 不要从Tree实例中获取节点。因为不存在。
            }
            else
            {
                node = TreeView?.Tree?.GetNodeByGuid(viewDataKey);
                this.Node = node;
            }

            var type = node?.GetType();
            var typeName = type?.Name ?? "NullNode";
            name = typeName;

            var titleString = node.GetTitleFromITitleableAndDisplayNameAttribute();
            if (node.Meta != null && node.Meta.TryGetName(out var metaName))
            {
                titleString += $"\n{metaName}";
            }

            title = titleString;

            RefreshDetail();
            RefreshBodyExpend();
            RefreshNodeIndex();
            RefreshAbortTypeUI();

            //使用自定义图标
            Icon.TrySetIconFromAttribute(type);
            RefreshDynamicMarker();

            ShortGUID.text = node?.GUID.Substring(0, 13) ?? string.Empty;
            SONode = CreateSOWrapperIfNull(node, forceReCreateSoWrapper);

            SONode.View = this;
            SONode.Node = node;
            SONode.name = typeName;

            //SetNodeScript();

            RefreshDecoratorListView();

            //是不是开始节点
            var isStartNode = TreeView.SOTree?.Tree?.IsStartNodeByGuid(node.GUID) ?? false;
            this.SetToClassList(StartNodeClass, isStartNode);

            var houdai = TreeView.SOTree?.Tree?.IsStartNodeDescendant(node) ?? false;
            //未连接的节点，保存但是运行时没有作用。
            this.SetToClassList("notConnected", !houdai && !isStartNode);

            if (!isStartNode)
            {
                //非开始节点使用自定义颜色
                var attri = type?.GetCustomAttribute<ColorAttribute>();
                if (attri != null)
                {
                    Body.style.backgroundColor = attri.Color;
                }
            }

            RefeshEnabled();

            CreatePort(node);
            AddToClassList(typeName);

            UpdateNodeType();

            ValidNodeData();
        }

        public void RefreshBodyExpend()
        {
            //增加用户自定义面板扩展Body部分
            if (Node is IViewBodyExpandable expandable)
            {
                BodyExpendContainer.Clear();
                var ex = expandable.GetBodyExpend(this);
                BodyExpendContainer.Add(ex);
            }
        }

        internal protected void ValidNodeData()
        {
            //验证节点参数
            if (Node is IDataValidable validable)
            {
                var (Result, ToolTip) = validable.Valid();
                this.SetToClassList(UssClassConst.dataWarning, Result != 0);
                titleContainer.tooltip = ToolTip;
            }
        }

        /// <summary>
        /// 设置IInspector面板增加脚本字段
        /// </summary>
        public async void SetNodeScript()
        {
            if (SONode?.Node != null && SONode.NodeScript == null)
            {
                var sc = await SONode.Node.GetType().GetMonoScript();
                SONode.NodeScript = sc;
            }
        }

        void UpdateNodeType()
        {
            this.SetToClassList(nameof(BTActionNode), Node is BTActionNode);
            this.SetToClassList(nameof(BTParentNode), Node is BTParentNode);
            this.SetToClassList(nameof(BTConditionNode), Node is BTConditionNode);
        }

        /// <summary>
        /// Debug是动态刷新的UI。
        /// </summary>
        public void RefreshOnDebug()
        {
            RefreshDetail();
            RefreshBodyExpend();
        }

        internal void RefreshDetail()
        {
            var showDetail = false;
            if (Detail != null)
            {
                if (Node is IDetailable detailable)
                {
                    var detail = detailable.GetDetail();
                    if (!string.IsNullOrEmpty(detail))
                    {
                        showDetail = true;
                    }
                    Detail.text = detail;

                    if (Node is IDetailAlignable alignable)
                    {
                        Detail.style.unityTextAlign = alignable.DetailTextAlign;
                    }
                }
            }
            DetailContainer.SetToClassList(UssClassConst.displayDetail, showDetail);
        }

        internal void RefreshDynamicMarker()
        {
            var showDynamicMarker = false;
            if (DynamicMarker != null)
            {
                if (Node is IDynamicable parentNode)
                {
                    showDynamicMarker = parentNode.Dynamic;
                }
                DynamicMarker.SetToClassList(UssClassConst.isDynamic, showDynamicMarker);
            }
        }

        internal void RefreshNodeIndex()
        {
            if (Node != null)
            {
                Index.text = Node.Index.ToString();
            }
        }

        internal void RefeshEnabled()
        {
            //自己或者父有没有关闭的节点
            bool isMute = false;
            if (Node != null)
            {
                if (Node.Enabled == false)
                {
                    isMute = true;
                }
                else
                {
                    List<BTParentNode> nodes = new();
                    if (Node.TryGetFirstExetutePath(nodes))
                    {
                        foreach (var node in nodes)
                        {
                            if (node.Enabled == false)
                            {
                                isMute = true;
                                break;
                            }
                        }
                    }
                }
            }
            this.SetToClassList(UssClassConst.isMute, isMute);
        }

        public void RefreshAbortTypeUI()
        {
            AbortTypeButton.RefreshAbortTypeBadge(Node);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //this.LogMethodName(evt.ToStringReflection(), "\n", evt.triggerEvent.ToStringReflection());

            //evt.menu.AppendAction("TestNode1", a => { }, DropdownMenuAction.Status.Normal);
            //this.LogMethodName();
            base.BuildContextualMenu(evt);

            if (evt.target is BehaviorTreeNodeView)
            {
                evt.menu.AppendAction("Open Documentation _F1", a => AI.Editor.Utility.OpenDocumentation(Node?.GetType()),
                    Node?.GetType().TryGetAttribute<HelpURLAttribute>(out var _) ?? false ?
                    DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);

                evt.menu.AppendSeparator();

                evt.menu.AppendActionTODO("Breakpoint", a => { }, DropdownMenuAction.Status.Normal);
                evt.menu.AppendSeparator();

                evt.menu.AppendAction("Open Node Script", a => Node?.GetType().OpenScript(), DropdownMenuAction.Status.Normal);
                evt.menu.AppendActionTODO("Open Node View Script", a => { }, DropdownMenuAction.Status.Normal);
                evt.menu.AppendAction("Select Node Script", a => Node?.GetType().SelectScript(), DropdownMenuAction.Status.Normal);
                evt.menu.AppendSeparator();

                evt.menu.AppendAction("Set Start", a => SetStart(), GetSetStartStatus);
                evt.menu.AppendSeparator();

                BuildContextualMenuDecorator(evt);

                //Subtree
                evt.menu.AppendAction("Convert To/Subtree",
                    a => { TreeView.ConvertToSubtree(Node); },
                    (Node is ISubtreeTreeElement) ? DropdownMenuAction.Status.Disabled : DropdownMenuAction.Status.Normal);
                evt.menu.AppendAction("Convert To/Inline Node",
                    a => { TreeView.InlineSubtree(Node as ISubtreeTreeElement); },
                    (Node is ISubtreeTreeElement) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);

                AddConvetToMenu(evt);

                evt.menu.AppendSeparator();

                if (Node is IBuildContextualMenuable buildable)
                {
                    buildable.BuildContextualMenu(evt);
                }

                evt.menu.AppendSeparator();
                evt.menu.AppendAction("Properties...", a => SONode?.OpenPropertyEditor(), DropdownMenuAction.Status.Normal);
                evt.menu.AppendSeparator();
            }
        }

        public void AddConvetToMenu(ContextualMenuPopulateEvent evt)
        {
            //Sequence  Selector 快速互换
            if (Node is Sequence sequence)
            {
                evt.menu.AppendAction("Convert To/Selector",
                a =>
                {
                    using var undo = TreeView?.UndoBeginScope("Sequence to Selector");
                    Selector newNode = new Selector();
                    TreeView?.Tree?.DynamicReplace(Node, newNode);
                    TreeView?.ReloadAllNodeView();
                    //为了防止错误，多更新一次序号。
                    TreeView?.SortAllChild();
                    TreeView?.Tree?.UpdateNodeIndexDepth();
                },
                DropdownMenuAction.Status.Normal);
            }

            if (Node is Selector selector)
            {
                evt.menu.AppendAction("Convert To/Sequence",
                a =>
                {
                    using var undo = TreeView?.UndoBeginScope("Selector to Sequence");
                    Sequence newNode = new Sequence();
                    TreeView?.Tree?.DynamicReplace(Node, newNode);
                    TreeView?.ReloadAllNodeView();
                    //为了防止错误，多更新一次序号。
                    TreeView?.SortAllChild();
                    TreeView?.Tree?.UpdateNodeIndexDepth();
                },
                DropdownMenuAction.Status.Normal);
            }
        }

        public DropdownMenuAction.Status GetSetStartStatus(DropdownMenuAction arg)
        {
            if (SONode?.Node == null)
            {
                return DropdownMenuAction.Status.Disabled;
            }

            var isStart = TreeView?.SOTree?.Tree.IsStartNodeByGuid(SONode.Node.GUID) ?? false;
            if (isStart)
            {
                return DropdownMenuAction.Status.Checked | DropdownMenuAction.Status.Disabled;
            }
            else
            {
                return DropdownMenuAction.Status.Normal;
            }
        }

        public void SetStart()
        {
            TreeView.SetStartNode(this);
        }

        public Edge ConnectParentNodeView(BehaviorTreeNodeView parent)
        {
            return ConnectParentNodeView<BehaviorTreeEdge>(parent);
        }

        public T ConnectParentNodeView<T>(BehaviorTreeNodeView parent)
            where T : Edge, new()
        {
            var edge = InputPort.ConnectTo<T>(parent.OutputPort);
            TreeView.AddElement(edge);
            return edge;
        }

        protected void OnToolTip(TooltipEvent evt)
        {
            evt.rect = this.worldBound;

            if (Node.TryGetToolTipString(out var tooltip))
            {
                evt.tooltip = tooltip;
            }

            evt.StopPropagation();
        }

        protected virtual void OnKeyDownEvent(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.M)
            {
                if (Node != null)
                {
                    Node.Enabled = !Node.Enabled;
                    TreeView.RefreshAllNodeEnabled();
                }
            }
        }
    }

    partial class BehaviorTreeNodeView : INodeView
    {
        ITreeView INodeView.TreeView => TreeView;
    }
}
