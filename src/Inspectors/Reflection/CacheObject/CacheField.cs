﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityExplorer.UI;
using UnityExplorer.Helpers;

namespace UnityExplorer.Inspectors.Reflection
{
    public class CacheField : CacheMember
    {
        public override bool IsStatic => (MemInfo as FieldInfo).IsStatic;

        public CacheField(FieldInfo fieldInfo, object declaringInstance) : base(fieldInfo, declaringInstance)
        {
            base.InitValue(null, fieldInfo.FieldType);

            UpdateValue();
        }

        public override void UpdateValue()
        {
            //if (IValue is InteractiveDictionary iDict)
            //{
            //    if (!iDict.EnsureDictionaryIsSupported())
            //    {
            //        ReflectionException = "Not supported due to TypeInitializationException";
            //        return;
            //    }
            //}

            try
            {
                var fi = MemInfo as FieldInfo;
                IValue.Value = fi.GetValue(fi.IsStatic ? null : DeclaringInstance);

                base.UpdateValue();
            }
            catch (Exception e)
            {
                ReflectionException = ReflectionHelpers.ExceptionToString(e);
            }
        }

        public override void SetValue()
        {
            var fi = MemInfo as FieldInfo;
            fi.SetValue(fi.IsStatic ? null : DeclaringInstance, IValue.Value);
        }
    }
}