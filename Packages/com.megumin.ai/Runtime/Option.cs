﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Megumin.AI
{
    public interface IRunOption
    {
        bool Log { get; }
        int Order { get; }
        OperationTree OnSucceeded { get; }
        OperationTree OnFailed { get; }
    }

    [Flags]
    public enum TickMode
    {
        None = 0,
        Update = 1 << 0,
        LateUpdate = 1 << 1,
        FixedUpdate = 1 << 2,
        Manual = 1 << 3,
    }

    [Serializable]
    public class RunOption
    {
        //public TickMode TickMode = TickMode.Update;
        public Enable<int> FrameInterval = new(false, 1);
        public Enable<double> TimeInterval = new(false, 0.1);
        public Enable<double> RealtimeInterval = new(false, 0.1);

        [field: Space]
        [field: SerializeField]
        public bool Log { get; set; }

        [field: SerializeField]
        public int Order { get; set; }

        [field: SerializeField]
        public OperationTree OnSucceeded { get; set; } = OperationTree.None;

        [field: SerializeField]
        public OperationTree OnFailed { get; set; } = OperationTree.None;
    }

    [Flags]
    public enum OperationTree
    {
        None = 0,
        ReSet = 1 << 0,
        ReStart = 1 << 1,
        Init = 1 << 2,
        Destory = 1 << 3,
        Pause = 1 << 4,
        Resume = 1 << 5,
        Enable = 1 << 6,
        Disable = 1 << 7,
        Start = 1 << 8,
        Stop = 1 << 9,
    }

    [Serializable]
    public class InitOption
    {
        /// <summary>
        /// 使用多线程异步实例化，防止阻塞主线程。
        /// 缺点是不会在当前帧立刻完成并执行行为树。
        /// </summary>
        [Tooltip("Not valid on WebGL platform, does not support multithreading")]
        public bool AsyncInit = true;
        /// <summary>
        /// 实例化之后，绑定代理对象前，延迟帧数。
        /// </summary>
        public Enable<int> BeforeBindAgentDelayFrame = new(false, 1);
        /// <summary>
        /// 绑定代理对象之后，解析可绑定对象前，延迟帧数。
        /// </summary>
        public Enable<int> BeforeParseBindingDelayFrame = new(false, 1);
        /// <summary>
        /// 运行时通常不会修改meta信息，也不会修改树结构。可以考虑共享meta。
        /// </summary>
        public bool SharedMeta = true;
        /// <summary>
        /// 延迟实例化子树，推迟到子树节点运行时实例化。
        /// </summary>
        public bool LazyInitSubtree = false;
        /// <summary>
        /// 使用生成的代码
        /// </summary>
        public bool UseGenerateCode = false;
        /// <summary>
        /// 开始执行树之前，最小延迟帧数
        /// </summary>
        public Enable<int> DelayMinFrame = new(false, 1);
        /// <summary>
        /// 开始执行树之前，延迟随机帧数
        /// </summary>
        public Enable<int> DelayRandomFrame = new(false, 30);
    }

    /// <summary>
    /// 初始化回调事件
    /// </summary>
    /// <typeparam name="Tree"></typeparam>
    /// <typeparam name="Runner"></typeparam>
    [Serializable]
    public class InitEvent<Tree, Runner>
    {
        public UnityEvent<Tree, Runner> BeforeInstantiate;
        public UnityEvent<Tree, Runner> AfterInstantiate;

        public UnityEvent<Tree, Runner> BeforeBindAgent;
        public UnityEvent<Tree, Runner> AfterBindAgent;

        public UnityEvent<Tree, Runner> BeforeParseBinding;
        public UnityEvent<Tree, Runner> AfterParseBinding;

        public UnityEvent<Tree, Runner> BeforeAddToManager;
        public UnityEvent<Tree, Runner> AfterAddToManager;

        public UnityEvent<Tree, Runner> BeforeRemoveFromManager;
        public UnityEvent<Tree, Runner> AfterRemoveFromManager;
    }

}




