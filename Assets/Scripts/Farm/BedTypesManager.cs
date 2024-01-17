using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BedTypesManager : MonoBehaviour
{
    [SerializeField] private List<BedType> _haveBeds;
    public List<BedType> HaveBeds => _haveBeds;

    public event Action<BedType> TypeAdded;

    public void AddType(BedType newBed)
    {
        _haveBeds.Add(newBed);
        _haveBeds = _haveBeds.OrderBy(bed => (int) bed.AcceptableType).ToList();
        TypeAdded?.Invoke(newBed);
    }
}
