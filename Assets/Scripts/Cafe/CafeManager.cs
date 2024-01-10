using UnityEngine;

public class CafeManager : MonoBehaviour
{
    [SerializeField] private CafeSpot[] _spots;
    private int _spotCount = 8;

    public int SpotCount => _spotCount;

    public float GetSpotSize()
    {
        return _spots[0].transform.localScale.x;
    }

    public void SetSpots()
    {

    }
}
