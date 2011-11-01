﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Rolcore.Tests.MockObjects
{
    class ReflectionUtilsMockObject
    {
        private bool _VoidMethodCalled = false;
        private bool _VoidMethodWithAttributeCalled = false;

        public ReflectionUtilsMockObject SubObject
        {
            get
            {
                ReflectionUtilsMockObject result = new ReflectionUtilsMockObject();
                result.IntPropNonNullable = this.IntPropNonNullable;
                result.IntPropNullable = this.IntPropNullable;
                result.StringProp = this.StringProp;
                Debug.Assert(result.Equals(this));
                return result;
            }
        }
        public int IntPropNonNullable { get; set; }
        public int? IntPropNullable{ get; set; }

        public string StringProp{ get; set; }

        public bool VoidMethodCalled
        {
            get
            {
                return _VoidMethodCalled;
            }
        }

        public bool VoidMethodWithAttributeCalled
        {
            get
            {
                return _VoidMethodWithAttributeCalled;
            }
        }

        public void VoidMethod()
        {
            Debug.WriteLine("VoidMethod()");
            this._VoidMethodCalled = true;
        }

        [ReflectionUtilsMock("")]
        public void VoidMethodWithAttribute()
        {
            Debug.WriteLine("VoidMethodWithAttribute()");
            this._VoidMethodWithAttributeCalled = true;
        }

        public override bool Equals(object obj)
        {
            ReflectionUtilsMockObject asMock = obj as ReflectionUtilsMockObject;
            if (asMock != null)
                return base.Equals(asMock)
                    || (asMock.IntPropNonNullable == this.IntPropNonNullable
                        && asMock.IntPropNullable == this.IntPropNullable
                        && asMock.StringProp == this.StringProp);
            return base.Equals(obj);
        }

        public override int GetHashCode() // Satisfy "overrides Object.Equals" warning.
        {
            return base.GetHashCode();
        }
    }
}