using System.Collections.Generic;
using UnityEngine;

public class TechnicManager : MonoBehaviour
{
    [SerializeField] private TechnicHolder[] _holders;
    [SerializeField] private List<Technic> _availableTechnic;
    [SerializeField] private TechnicHolderUI _UI;

    private void Start()
    {
        foreach (var holder in _holders) {
            holder.gameObject.SetActive(_availableTechnic.Contains(holder.Technic));
            holder.SetUI(_UI);
        }
    }

    public bool HaveTechnic(Technic technic)
    {
        if (!_availableTechnic.Contains(technic))
            return false;
        var holder = FindHolderByTechic(technic);
        return holder.IsFree && holder.NowStrength != 0 && !holder.IsRepairing;
    }

    public void ActivateTechnic(Order order)
    {
        var technic = FindHolderByTechic(order.Food.TypeTechnic);
        technic.StartCook(order);
    }

    public void DisableTechnic(Technic typeTechnic)
    {
        var technic = FindHolderByTechic(typeTechnic);
        technic.StopCook();
    }

    public TechnicHolder FindHolderByTechic(Technic technic)
    {
        for (int i = 0; i < _holders.Length; i++)
            if (_holders[i].Technic == technic)
                return _holders[i];
        return null;
    }
}
