using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodChoice : MonoBehaviour
{
    public Client client;
    private bool OpenFood = false;
    public GameObject ButtonsYesNo;
    public AudioSource Xm;
    public AudioSource ClickVol;
    public bool Ed;

    public void Click()
    {
        if (NewOrContinue.Education && !Ed) { 
            client.cafe.education.AddIndex(false);
            Ed = true;
        }
        if (!OpenFood) {
            Xm.Play();
            var ChoisedFood = client.cafe.AvailableFood[Random.Range(0, client.cafe.AvailableFood.Count)];
            this.GetComponent<Image>().sprite = ChoisedFood.ImageFood;
            client.cafe.orders.AddFood(ChoisedFood, client);
            client.NeedFood = ChoisedFood;
            OpenFood = true;
        } else if (!client.Eating) {
            ClickVol.Play();
            ButtonsYesNo.SetActive(!ButtonsYesNo.activeSelf);
        }
    }
}
