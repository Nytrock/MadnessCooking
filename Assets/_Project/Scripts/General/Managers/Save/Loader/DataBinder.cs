using AYellowpaper;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    public abstract class DataBinder<TData> : MonoBehaviour where TData : ISaveable {

        [SerializeField] private InterfaceReference<IBindable<TData>>[] _bindables;

        public event Action BeforeLateStart;
        public event Action AfterLateStart;

        public virtual void Bind(TData data) {
            foreach (var bindable in _bindables)
                bindable.Value.Bind(data);

            BeforeLateStart?.Invoke();
            foreach (var bindable in _bindables)
                bindable.Value.LateStart();
            AfterLateStart?.Invoke();
        }
    }
}
