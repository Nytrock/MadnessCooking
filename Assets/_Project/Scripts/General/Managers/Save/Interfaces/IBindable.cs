public interface IBindable<TData>
    where TData : ISaveable {

    void LateStart();
    void Bind(TData data);
}
