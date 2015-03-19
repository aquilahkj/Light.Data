using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Light.Data.Handler
{
    delegate void SetValueHandler(object source, object value);
    delegate object GetValueHandler(object source);
    delegate object ObjectInstanceHandler();
    delegate object FastMethodHandler(object target, object[] paramters);

    class PropertyHandler
    {
        private GetValueHandler mGetValue;
        private PropertyInfo mProperty;
        private SetValueHandler mSetValue;

        public PropertyHandler(PropertyInfo property)
        {
            if (property.CanWrite)
            {
                this.mSetValue = ReflectionHandlerFactory.PropertySetHandler(property);
            }
            if (property.CanRead)
            {
                this.mGetValue = ReflectionHandlerFactory.PropertyGetHandler(property);
            }
            this.mProperty = property;
            this.IndexProperty = this.mProperty.GetGetMethod().GetParameters().Length > 0;
        }

        public GetValueHandler Get
        {
            get
            {
                return this.mGetValue;
            }
        }

        public bool IndexProperty { get; set; }

        public PropertyInfo Property
        {
            get
            {
                return this.mProperty;
            }
            set
            {
                this.mProperty = value;
            }
        }

        public SetValueHandler Set
        {
            get
            {
                return this.mSetValue;
            }
        }
    }
}
