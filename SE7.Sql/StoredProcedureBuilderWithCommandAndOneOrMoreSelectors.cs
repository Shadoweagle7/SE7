using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Sql
{
    public class StoredProcedureBuilderWithCommandAndOneOrMoreSelectors : StoredProcedure
    {
        private readonly List<StoredProcedureSelector> Selectors = [];

        public StoredProcedureBuilderWithCommandAndOneOrMoreSelectors(
            string connectionString,
            Type dataTransferObjectType,
            StoredProcedureSelector storedProcedureSelector
        ) : base(connectionString, dataTransferObjectType)
        {
            Selectors.Add(storedProcedureSelector);
        }

        protected override IList<StoredProcedureSelector> GetStoredProcedureSelectors() => Selectors;

        public StoredProcedureBuilderWithCommandAndOneOrMoreSelectors And<TStoredProcedureSelector>(
            TStoredProcedureSelector storedProcedureSelector
        )
            where TStoredProcedureSelector : StoredProcedureSelector
        {
            Selectors.Add(storedProcedureSelector);

            return this;
        }
    }
}
