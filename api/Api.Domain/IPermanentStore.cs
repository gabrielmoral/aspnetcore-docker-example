namespace Api.Domain
{
    public interface IPermanentStore
    {
        ValueType Get(int value);
        void Save(ValueType valueType);
    }
}