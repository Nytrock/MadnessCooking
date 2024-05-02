using System.Collections.Generic;
using UnityEngine;

public class DecorManager : MonoBehaviour
{
    [SerializeField] private DecorHolder[] decorHolders;
    private List<Decor> _haveDecors = new();

    private void Start()
    {
        UpdateDecorHolders();
    }

    public void AddDecor(Decor decor)
    {
        _haveDecors.Add(decor);
        FindAndActivateHolder(decor);
    }

    private void UpdateDecorHolders()
    {
        foreach (var holder in decorHolders)
            holder.ChangeState(_haveDecors.Contains(holder.Decor));
    }

    private void FindAndActivateHolder(Decor decor)
    {
        foreach (var holder in decorHolders) {
            if (holder.Decor == decor) {
                holder.ChangeState(true);
                break;
            }
        }
    }
}
