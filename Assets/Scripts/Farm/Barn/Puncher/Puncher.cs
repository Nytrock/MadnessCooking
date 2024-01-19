using System;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    private int _count = 0;
    public int Count => _count;

    public event Action<int> FertilizeAdded;

    public void SubtractFertilize()
    {

    }
}
