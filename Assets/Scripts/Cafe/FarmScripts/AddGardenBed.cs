using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddGardenBed : MonoBehaviour
{
    public Transform Container;
    public GameObject GardenBeds;
    public GameObject AddGardenBeds;
    public MoneyBar money;
    public Button ThisButon;
    public List<int> Counts;
    public List<GameObject> Beds;
    public int Index = 0;
    public Text TextCount;
    public AudioSource AddVoice;

    void Update()
    {
        if (money.Coins >= Counts[Index])
            ThisButon.interactable = true;
        else
            ThisButon.interactable = false;
        TextCount.text = Counts[Index].ToString();
    }

    public void Add()
    {
        AddVoice.Play();
        money.AddCoins(-Counts[Index]);
        Beds[Index].SetActive(true);
        Index += 1;
        if (Index == Counts.Count)
            Destroy(this.gameObject);
    }
}
