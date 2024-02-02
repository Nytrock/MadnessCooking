using System.Collections.Generic;
using UnityEngine;

public class TechnicManager : MonoBehaviour
{
    [SerializeField] private TechnicHolder[] _holders;
    [SerializeField] private List<Technic> _availableTechnic;

    private void Start()
    {
        foreach (var holder in _holders)
            holder.gameObject.SetActive(_availableTechnic.Contains(holder.Technic));
    }

    public bool HaveTechnic(Technic technic)
    {
        if (!_availableTechnic.Contains(technic))
            return false;
        return FindHolderByTechic(technic).IsFree;
    }

    public void ActivateTechnic(Food food)
    {
        var technic = FindHolderByTechic(food.TypeTechnic);
        technic.StartWork(food);
    }

    public void DisableTechnic(Technic typeTechnic)
    {
        var technic = FindHolderByTechic(typeTechnic);
        technic.MakeFree();
    }

    private TechnicHolder FindHolderByTechic(Technic technic)
    {
        for (int i = 0; i < _holders.Length; i++)
            if (_holders[i].Technic == technic)
                return _holders[i];
        return null;
    }

}
