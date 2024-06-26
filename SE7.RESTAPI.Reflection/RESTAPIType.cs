using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SE7.RESTAPI.Reflection
{
    public class RESTAPIType : Type
    {
        private readonly RESTAPIAssembly RESTAPIAssembly;
        private readonly string RESTAPITypeName;

        // Usually, DI only works with 1 constructor - but logically, it can work with more.
        private readonly IEnumerable<RESTAPIConstructorInfo> RESTAPIConstructors;
        private readonly IEnumerable<RESTAPIMethodInfo> RESTMethodInfos;

        public override Assembly Assembly => RESTAPIAssembly;

        // E.g. "System.Int32, System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"
        public override string? AssemblyQualifiedName => $"{}";
        public override Type? BaseType { get; }
        public override string? FullName => throw new NotImplementedException();
        public override Guid GUID => throw new NotImplementedException();
        public override Module Module => throw new NotImplementedException();
        public override Type UnderlyingSystemType => throw new NotImplementedException();
        public override string Name => RESTAPITypeName;

        internal RESTAPIType(
            RESTAPIAssembly restAPIAssembly,
            Type? baseType,
            string restAPITypeName,
            IEnumerable<RESTAPIConstructorInfo> constructors,
            IEnumerable<RESTAPIMethodInfo> restAPIMethodInfos
        )
        {
            RESTAPIAssembly = restAPIAssembly;
            BaseType = baseType;
            RESTAPITypeName = restAPITypeName;
            RESTAPIConstructors = constructors;
            RESTMethodInfos = restAPIMethodInfos;
        }

        public static RESTAPIType Of(???)
        {

        }

        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override Type? GetElementType()
        {
            throw new NotImplementedException();
        }

        public override EventInfo? GetEvent(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override FieldInfo? GetField(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
        public override Type? GetInterface(string name, bool ignoreCase)
        {
            throw new NotImplementedException();
        }

        public override Type[] GetInterfaces()
        {
            throw new NotImplementedException();
        }

        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type? GetNestedType(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override object? InvokeMember(string name, BindingFlags invokeAttr, Binder? binder, object? target, object?[]? args, ParameterModifier[]? modifiers, CultureInfo? culture, string[]? namedParameters)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        protected override TypeAttributes GetAttributeFlagsImpl()
        {
            throw new NotImplementedException();
        }

        protected override ConstructorInfo? GetConstructorImpl(BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[] types, ParameterModifier[]? modifiers)
        {
            throw new NotImplementedException();
        }

        protected override MethodInfo? GetMethodImpl(string name, BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[]? types, ParameterModifier[]? modifiers)
        {
            throw new NotImplementedException();
        }

        protected override PropertyInfo? GetPropertyImpl(string name, BindingFlags bindingAttr, Binder? binder, Type? returnType, Type[]? types, ParameterModifier[]? modifiers)
        {
            throw new NotImplementedException();
        }

        protected override bool HasElementTypeImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsArrayImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsByRefImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsCOMObjectImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPointerImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPrimitiveImpl()
        {
            throw new NotImplementedException();
        }
    }
}
