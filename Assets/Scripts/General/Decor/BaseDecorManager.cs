using UnityEngine;

public class BaseDecorManager : MonoBehaviour {
    [SerializeField] protected DecorHolder[] _decorHolders;

    private void Awake() {
        foreach (var holder in _decorHolders)
            holder.ChangeState(false);
    }

    public virtual void AddDecor(Decor decor) {
        DecorHolder holder = TryFindHolder(decor);
        if (holder != null)
            holder.ChangeState(true);
    }

    protected DecorHolder TryFindHolder(Decor decor) {
        foreach (var holder in _decorHolders)
            if (holder.Decor == decor)
                return holder;

        return null;
    }
}
