namespace Api.Domain
{
    public interface IMemoryStore
    {
        ValueType Get(int value);
        void Save(ValueType valueType);
    }
}