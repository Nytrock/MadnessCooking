public interface IBindable<TData> where TData : ISaveable
{
    void SetData(TData data);
    void Bind(TData data);
}
