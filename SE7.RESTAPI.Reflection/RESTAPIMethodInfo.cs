using System.Globalization;
using System.Reflection;

namespace SE7.RESTAPI.Reflection
{
    public class RESTAPIMethodInfo : MethodInfo
    {
        private class RESTAPIMethodInfoCustomAttributeProvider : ICustomAttributeProvider
        {
            public static RESTAPIMethodInfoCustomAttributeProvider Instance { get; } = new();

            public object[] GetCustomAttributes(bool inherit)
            {
                throw new NotImplementedException();
            }

            public object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }

            public bool IsDefined(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }
        }

        private readonly RESTAPIParameterInfo[] ParameterInfos;

        public override ICustomAttributeProvider ReturnTypeCustomAttributes => RESTAPIMethodInfoCustomAttributeProvider.Instance;

        public override MethodAttributes Attributes => MethodAttributes.Public;

        public override RuntimeMethodHandle MethodHandle { get; }

        public override Type? DeclaringType { get; }

        public override string Name { get; }

        public override Type? ReflectedType { get; }

        internal RESTAPIMethodInfo(
            RuntimeMethodHandle runtimeMethodHandle,
            Type? declaringType,
            string name,
            Type? reflectedType,
            RESTAPIParameterInfo[] restAPIParameterInfos
        )
        {
            MethodHandle = runtimeMethodHandle;
            DeclaringType = declaringType;
            Name = name;
            ReflectedType = reflectedType;
            ParameterInfos = restAPIParameterInfos;
        }

        public override MethodInfo GetBaseDefinition() => throw new InvalidOperationException("Invalid operation for a REST Method.");

        public override object[] GetCustomAttributes(bool inherit) =>
            ReturnTypeCustomAttributes.GetCustomAttributes(inherit);

        public IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(bool inherit) where TAttribute : Attribute =>
            GetCustomAttributes(inherit).Cast<TAttribute>();

        public override object[] GetCustomAttributes(Type attributeType, bool inherit) =>
            ReturnTypeCustomAttributes.GetCustomAttributes(attributeType, inherit);

        public IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(Type attributeType, bool inherit)
            where TAttribute : Attribute =>
            GetCustomAttributes(attributeType, inherit).Cast<TAttribute>();

        public override MethodImplAttributes GetMethodImplementationFlags()
            => MethodImplAttributes.Managed;

        public override ParameterInfo[] GetParameters() => ParameterInfos;

        public override object? Invoke(object? obj, BindingFlags invokeAttr, Binder? binder, object?[]? parameters, CultureInfo? culture)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit) => ReturnTypeCustomAttributes.IsDefined(attributeType, inherit);
    }
}
