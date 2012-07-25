using System;
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
        private ReflectionUtilsMockObject _Parent;

        public ReflectionUtilsMockObject(ReflectionUtilsMockObject parent = null)
        {
            _Parent = parent;
        }

        public ReflectionUtilsMockObject SubObject
        {
            get
            {
                if (_Parent != null)
                    return null; // Lest we recurse forever

                ReflectionUtilsMockObject result = new ReflectionUtilsMockObject(this);
                result.IntPropNonNullable = this.IntPropNonNullable;
                result.IntPropNullable = this.IntPropNullable;
                result.StringProp = this.StringProp;
                Debug.Assert(result.Equals(this));
                return result;
            }
        }
        public DateTime DateTimeProp { get; set; }
        public DateRange DateRangeProp { get; set; }
        public int IntPropNonNullable { get; set; }
        public int? IntPropNullable{ get; set; }
        public string StringProp{ get; set; }
        public string[] StringArrayProp { get; set; }

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
            {
                bool result = base.Equals(asMock)
                    || (asMock.IntPropNonNullable == this.IntPropNonNullable
                        && asMock.IntPropNullable == this.IntPropNullable
                        && asMock.StringProp == this.StringProp
                        && asMock.DateTimeProp == this.DateTimeProp
                        && asMock.DateRangeProp == this.DateRangeProp);
                result = result && (asMock.StringArrayProp.Length == this.StringArrayProp.Length);
                if (result)
                {
                    for (int i = 0; i < asMock.StringArrayProp.Length; i++)
                        result = result && asMock.StringArrayProp[i] == this.StringArrayProp[i];
                }
                return result;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode() // Satisfy "overrides Object.Equals" warning.
        {
            return base.GetHashCode();
        }
    }
}
