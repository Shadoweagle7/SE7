namespace SE7.Utility
{
    internal class TypeSwitch
    {
        private readonly Dictionary<Type, Action<Type>> Cases = [];
        private Action? DefaultCase;

        public TypeSwitch Case<T>(Action<Type> action)
        {
            Cases.Add(typeof(T), action);

            return this;
        }

        public TypeSwitch Default(Action action)
        {
            DefaultCase = action;

            return this;
        }

        public void Execute<T>()
        {
            if (Cases.TryGetValue(typeof(T), out var caseAction))
            {
                caseAction(typeof(T));
            }
            else
            {
                DefaultCase?.Invoke();
            }
        }
    }
}
