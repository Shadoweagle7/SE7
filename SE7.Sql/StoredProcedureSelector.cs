using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Sql
{
    public abstract record StoredProcedureSelector
    {
        public abstract object GetTypeErasedValue();
        public virtual string GetParameterName() => GetType().Name;
    }
}
