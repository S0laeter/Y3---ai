﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Megumin.AI.BehaviorTree
{
    [Category("Debug")]
    [HelpURL(URL.WikiTask + "Empty")]
    public sealed class Empty : OneChildNode
    {
        protected override Status OnTick(BTNode from, object options = null)
        {
            if (Child0 != null)
            {
                return Child0.Tick(this);
            }
            return GetIgnoreResult(from);
        }
    }
}
