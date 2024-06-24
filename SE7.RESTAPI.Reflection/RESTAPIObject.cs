using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.RESTAPI.Reflection
{
    public class RESTAPIObject
    {
        public new Type GetType()
        {
            return RESTAPIType.Of();
        }
    }
}
