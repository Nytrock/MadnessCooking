using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public sealed class InterfaceAttribute : PropertyAttribute {
    public Type InterfaceType { get; }

    public InterfaceAttribute(Type interfaceType) {
        if (!interfaceType.IsInterface)
            throw new ArgumentException($"{interfaceType.FullName} must be an interface.", nameof(interfaceType));
        InterfaceType = interfaceType;
    }
}