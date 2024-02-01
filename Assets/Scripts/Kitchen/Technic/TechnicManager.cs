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
        return _availableTechnic.Contains(technic);
    }
}
