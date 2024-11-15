namespace SE7.RESTAPI.Reflection
{
    internal class RESTAPITypeCache
    {
        readonly struct RESTAPIObjectIdentifier
        {
            private readonly long Value;

            public RESTAPIObjectIdentifier(long value) => Value = value;

            public static implicit operator long(RESTAPIObjectIdentifier value) => value.Value;
        }

        private readonly Dictionary<RESTAPIObjectIdentifier, RESTAPIType> Cache;

        public RESTAPIType GetOrAdd<TRESTAPIObject>() where TRESTAPIObject : RESTAPIObject
        {

        }
    }
}
