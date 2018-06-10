namespace Api.Domain
{
    public interface IValuesProcessor
    {
        ValueType Get(int value);
        void Save(ValueType valueType);
    }

    public class ValuesProcessor : IValuesProcessor
    {
        private readonly IPermanentStore _permanentStore;
        private readonly IMemoryStore _memoryStore;

        public ValuesProcessor(IPermanentStore permanentStore, IMemoryStore memoryStore)
        {
            _permanentStore = permanentStore;
            _memoryStore = memoryStore;
        }

        public ValueType Get(int value)
        {
            var inMemoryValue = _memoryStore.Get(value);

            if (inMemoryValue != null)
            {
                return inMemoryValue;
            }
            
            return _permanentStore.Get(value);
        }

        public void Save(ValueType valueType)
        {
            _permanentStore.Save(valueType);
            _memoryStore.Save(valueType);
        }
    }
}