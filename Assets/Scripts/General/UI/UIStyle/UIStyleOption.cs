using System;
using UnityEngine;

[Serializable]
public class UIStyleOption<TValue, TStyle> {
    [SerializeField] private TValue _value;
    [SerializeField] private TStyle _style;

    public TValue Value => _value;
    public TStyle Style => _style;
}
