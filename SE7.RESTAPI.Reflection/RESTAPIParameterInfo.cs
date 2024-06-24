using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SE7.RESTAPI.Reflection
{
    public class RESTAPIParameterInfo : ParameterInfo
    {
        public override ParameterAttributes Attributes { get; }
        public override IEnumerable<CustomAttributeData> CustomAttributes { get; }
        public override object? DefaultValue { get; }

        public RESTAPIParameterInfo(
            ParameterAttributes attributes,
            IEnumerable<CustomAttributeData> customAttributes
        )
        {
            AssertCorrectParameterAttributes(attributes);

            Attributes = attributes;
            CustomAttributes = customAttributes;
            DefaultValue = DBNull.Value;
        }

        public RESTAPIParameterInfo(
            ParameterAttributes attributes,
            IEnumerable<CustomAttributeData> customAttributes,
            object? defaultValue
        )
        {
            AssertCorrectParameterAttributes(attributes);

            Attributes = attributes;
            CustomAttributes = customAttributes;
            DefaultValue = defaultValue;
        }

        private static void AssertCorrectParameterAttributes(ParameterAttributes attributes)
        {
            if ((attributes & ParameterAttributes.In) != ParameterAttributes.In)
            {
                throw new ArgumentException("REST Method parameters must be input parameters.");
            }
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return base.GetCustomAttributes(inherit);
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return base.GetCustomAttributes(attributeType, inherit);
        }

        public override IList<CustomAttributeData> GetCustomAttributesData()
        {
            return base.GetCustomAttributesData();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Type GetModifiedParameterType()
        {
            return base.GetModifiedParameterType();
        }

        public override Type[] GetOptionalCustomModifiers()
        {
            return base.GetOptionalCustomModifiers();
        }

        public override Type[] GetRequiredCustomModifiers()
        {
            return base.GetRequiredCustomModifiers();
        }

        public override bool HasDefaultValue => base.HasDefaultValue;

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return base.IsDefined(attributeType, inherit);
        }

        public override MemberInfo Member => base.Member;

        public override int MetadataToken => base.MetadataToken;
        public override string? Name => base.Name;
        public override Type ParameterType => base.ParameterType;
        public override int Position => base.Position;
        public override object? RawDefaultValue => base.RawDefaultValue;
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
