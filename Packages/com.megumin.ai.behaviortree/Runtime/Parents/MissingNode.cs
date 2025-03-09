﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Megumin.Serialization;

namespace Megumin.AI.BehaviorTree
{
    /// <summary>
    /// 用于反序列化失败
    /// </summary>
    [Category("Debug")]
    [DisplayName("Missing")]
    public class MissingNode : BTParentNode, IDetailable
    {
        public string MissType { get; set; }
        public ObjectData OrignalData { get; set; }

        protected override Status OnTick(BTNode from, object options = null)
        {
            return GetIgnoreResult(from);
        }

        public string GetDetail()
        {
            return MissType;
        }
    }
}
