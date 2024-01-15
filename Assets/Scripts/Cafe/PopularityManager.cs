using UnityEngine;

public class PopularityManager : MonoBehaviour
{
    [SerializeField] private CafeSpaceManager _spaceManager;

    private int _popularity;

    public float GetPopularity()
    {
        return 1;
    }

    public void GetClientsNumberChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance)
    {
        singleChance = 1000;
        doubleChance = 0;
        tripleChance = 0;
        quarterChance = 0;

        doubleChance += singleChance;
        tripleChance += doubleChance;
        quarterChance += tripleChance;
    }

    public int GetPurePopularity()
    {
        return _popularity;
    }

    public float GetSpaceMultiplier()
    {
        return Mathf.Max(1, _spaceManager.SpaceCount * 0.375f);
    }
}
