using System;
using System.Collections.Generic;
using UnityEngine;

public class TechnicManager : MonoBehaviour
{
    [SerializeField] private TechnicHolder[] _holders;
    [SerializeField] private TechnicHolderUI _UI;
    [SerializeField] private List<Technic> _availableTechnic;

    public event Action TechnicChanged;

    private void Start()
    {
        foreach (var holder in _holders) {
            if (_availableTechnic.Contains(holder.Technic))
                holder.Activate(_UI);
            else
                holder.gameObject.SetActive(false);
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

    public void AddTechnic(Technic technic)
    {
        _availableTechnic.Add(technic);
        var holder = FindHolderByTechic(technic);
        holder.Activate(_UI);
        TechnicChanged?.Invoke();
    }
}
