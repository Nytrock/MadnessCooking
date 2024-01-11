using UnityEngine;

public class PopularityManager : MonoBehaviour
{
    public float GetPopularity()
    {
        return 1;
    }

    public void GetClientsNumberChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance)
    {
        singleChance = 1000;
        doubleChance = singleChance;
        tripleChance = doubleChance;
        quarterChance = tripleChance;
    }
}
