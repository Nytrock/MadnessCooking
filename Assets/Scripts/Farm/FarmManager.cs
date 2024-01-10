using UnityEngine;

public class FarmManager : MonoBehaviour
{
    [SerializeField] private FarmGroundbeds[] _beds;
    private int _bedsCount = 8;

    public int BedsCount => _bedsCount;

    public float GetBedSize()
    {
        return _beds[0].transform.localScale.y;
    }

    public void BedsSpots()
    {

    }
}
