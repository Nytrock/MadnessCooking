using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyBar : MonoBehaviour
{
    public TextMeshProUGUI CountCoins;
    public int Coins;
    public AudioSource MoneyVoice; 

    public void AddCoins(int newCoins)
    {
        Coins += newCoins;
        MoneyVoice.Play();
    }

    void Update()
    {
        CountCoins.text = Coins.ToString();
    }
}
