using System;
using UnityEngine;

public class LocationDecorManager : MonoBehaviour {
    [SerializeField] private DecorLocation _location;
    [SerializeField] protected DecorHolder[] _decorHolders;

    private void Awake() {
        foreach (var holder in _decorHolders)
            if (!holder.CheckLocation(_location))
                throw new ArgumentException($"Location of decor {holder.Decor.name} does not match");

        foreach (var holder in _decorHolders)
            holder.ChangeState(false);
    }

    public void AddDecor(Decor decor) {
        if (decor.Location != _location)
            return;

        foreach (var holder in _decorHolders)
            if (holder.Decor == decor)
                holder.ChangeState(true);
    }
}
