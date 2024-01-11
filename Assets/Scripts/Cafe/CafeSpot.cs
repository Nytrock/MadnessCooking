using UnityEngine;

public class CafeSpot : MonoBehaviour
{
    [SerializeField] private CafeSeat [] _seats;

    public int SeatsCount => _seats.Length;

    public Transform GetTarget(int index = 0) { return _seats[index].transform; }
}
