﻿namespace SE7.RESTAPI.Reflection
{
    public class RESTAPIObject
    {
        public new RESTAPIType GetType()
        {

        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
