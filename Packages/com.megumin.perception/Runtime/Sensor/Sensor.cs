﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;

namespace Megumin.Perception
{
    public interface ISensor
    {
        bool TryGetInSensor(HashSet<Collider> results, int maxColliders = 10);
    }

    /// <summary>
    /// 传感器组件
    /// </summary>
    public class Sensor : MonoBehaviour, ISensor
    {
        public GameObjectFilter Filter;


        public virtual bool TryGetInSensor(HashSet<Collider> results,
                                           int maxColliders = 10)
        {
            if (enabled == false)
            {
                return false;
            }

            return Filter.TryPhysicsTest(transform.position, GetRadius(), results, CheckCollider);
        }

        public virtual float GetRadius()
        {
            return 0;
        }

        /// <summary>
        /// 用于子类重写
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        public virtual bool CheckCollider(Collider collider)
        {
            return true;
        }

        static bool globalDebugshow = true;
        /// <summary>
        /// 全局显示开关
        /// </summary>
        public static bool GlobalDebugShow
        {
            get
            {
                //if (globalDebugshow == null)
                //{
                //    globalDebugshow = new Pref<bool>(nameof(Sensor), true);
                //}
                return globalDebugshow;
            }
        }

        [Editor]
        public void SwitchGlobalToggle()
        {
            globalDebugshow = !GlobalDebugShow;
        }
    }

    public class SensorType
    {
        public const string Sight = nameof(Sight);
        public const string Instinct = nameof(Instinct);
    }
}
