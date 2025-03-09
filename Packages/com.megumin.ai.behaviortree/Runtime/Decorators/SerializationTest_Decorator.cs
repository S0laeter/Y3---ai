﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.Reflection;
using Megumin.Serialization;
using UnityEngine;

namespace Megumin.AI.BehaviorTree
{
    [DisplayName("SerializationTest")]
    [SerializationAlias("Megumin.AI.BehaviorTree.SerializationTestDecorator")]
    [Category("Samples/Serialization")]
    public class SerializationTest_Decorator : BTDecorator, ISerializationCallbackReceiver<CollectionSerializationData>
    {
        public float TestFloat = 3f;
        public GameObject TestRef;
        public List<GameObject> TestList;
        public List<int> TestList2;
        public string TestCallbackReceiver;
        public MyClass TestCallbackReceiverMyClass;

        [Serializable]
        public class MyClass
        {
            public int a;
            public int b;
        }

        public void OnAfterDeserialize(List<CollectionSerializationData> source)
        {
            foreach (var item in source)
            {
                if (item.Name == nameof(TestCallbackReceiver))
                {
                    TestCallbackReceiver = item.Data;
                }

                if (item.Name == nameof(TestCallbackReceiverMyClass))
                {
                    TestCallbackReceiverMyClass = new MyClass();
                    var sp = item.Data.Split("|");
                    TestCallbackReceiverMyClass.a = int.Parse(sp[0]);
                    TestCallbackReceiverMyClass.b = int.Parse(sp[1]);
                }
            }
        }

        public void OnBeforeSerialize(List<CollectionSerializationData> desitination, List<string> ignoreMemberOnSerialize)
        {
            ignoreMemberOnSerialize.Add(nameof(TestCallbackReceiver));
            desitination.Add(new CollectionSerializationData()
            {
                Name = nameof(TestCallbackReceiver),
                Data = TestCallbackReceiver,
            });

            ignoreMemberOnSerialize.Add(nameof(TestCallbackReceiverMyClass));
            if (TestCallbackReceiverMyClass != null)
            {
                desitination.Add(new CollectionSerializationData()
                {
                    Name = nameof(TestCallbackReceiverMyClass),
                    Data = $"{TestCallbackReceiverMyClass.a}|{TestCallbackReceiverMyClass.b}",
                });
            }
        }
    }
}
