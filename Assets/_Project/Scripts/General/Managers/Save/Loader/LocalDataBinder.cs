using AYellowpaper;
using UnityEngine;

namespace MadnessCooking.General {
    public abstract class LocalDataBinder<TMainData, TData> : MonoBehaviour, IBindable<TMainData>
        where TMainData : ISaveable where TData : ISaveable {

        [SerializeField] protected InterfaceReference<IBindable<TData>>[] _bindables;
        protected TData _data;

        public void LateStart() {
            foreach (var bindable in _bindables)
                bindable.Value.LateStart();
        }

        public void Bind(TMainData data) {
            SetData(data);
            foreach (var bindable in _bindables)
                bindable.Value.Bind(_data);
        }

        protected abstract void SetData(TMainData data);
    }
}
