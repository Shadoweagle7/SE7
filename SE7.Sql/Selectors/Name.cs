using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Sql.Selectors
{
    internal record Name(string Value) : StoredProcedureSelector
    {
        public override object GetTypeErasedValue() => Value;

        public static implicit operator Name(string value) => new(value);
        public static implicit operator string(Name name) => name.Value;
    }
}
