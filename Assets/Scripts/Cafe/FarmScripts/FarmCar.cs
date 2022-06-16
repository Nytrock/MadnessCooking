using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmCar : MonoBehaviour
{
    public List<Ingridient> IngridientsInCar;
    public List<int> CountIngridientsCar;
    public CarTimer timer;
    public Animator animator;
    public Transform IngridientsContainer;
    public Transform TextContainer;
    public IngridientCell ingridientCell;
    public TextIngridient ingridientText;
    public ForIngridients ingridients;
    public ForOrders orders;
    public int Size;
    public int Fullness;
    public Text TextFullness;
    public AudioSource Open;
    public AudioSource Close;
    public AudioSource Drive;
    public Transform Cmera;
    public bool Driving;
    public Button Send;

    void Start()
    {
        TextFullness.text = Fullness.ToString() + "/" + Size.ToString();
    }

    public void CarToKitchen()
    {
        for(int i = 0; i < IngridientsInCar.Count; i++) {
            if (ingridients.HaveIngridients.Contains(IngridientsInCar[i])) {
                ingridients.NumberIngridients[ingridients.HaveIngridients.IndexOf(IngridientsInCar[i])] += CountIngridientsCar[i];
            } else {
                ingridients.HaveIngridients.Add(IngridientsInCar[i]);
                ingridients.NumberIngridients.Add(CountIngridientsCar[i]);
            }
        }
        ingridients.RenderIngridients();
        orders.UpgradeIngridientsOrders();
        IngridientsInCar.Clear();
        CountIngridientsCar.Clear();
        UpdateIngridients();
        Send.interactable = false;
        this.GetComponent<Animator>().SetBool("ToKitchen", true);
        timer.NowTime = timer.AllTime;
        animator.SetBool("Active", false);
        Fullness = 0;
    }
    
    public void OpenPanel()
    {
        animator.SetBool("Active", !animator.GetBool("Active"));
        if (animator.GetBool("Active"))
            Open.Play();
        else
            Close.Play();
    }

    public void UpdateIngridients()
    {
        foreach(Transform child in IngridientsContainer)
            Destroy(child.gameObject);
        int Count = 0;
        for (int i = 0; i < CountIngridientsCar.Count; i++) {
            var cell = Instantiate(ingridientCell, IngridientsContainer);
            cell.IngridientImage.sprite = IngridientsInCar[i].ImageIngridient;
            Count += CountIngridientsCar[i];
            cell.Count.text = CountIngridientsCar[i].ToString();
            var descr = Instantiate(ingridientText, TextContainer);
            descr.MainText.text = IngridientsInCar[i].Name;
            descr.DescriptionText.text = IngridientsInCar[i].Description;
            cell.Description = descr;
        }
        Fullness = Count;
        TextFullness.text = Fullness.ToString() + "/" + Size.ToString();
    }

    public void DriveVolume()
    {
        Driving = !Driving;
    }

    public void CarFrom()
    {
        this.GetComponent<Animator>().SetBool("ToKitchen", false);
    }

    public void Update()
    {
        if (Driving) {
            if (Cmera.position.x == -36.2f) {
                if (!Drive.isPlaying)
                    Drive.Play();
            } else {
                Drive.Stop();
            }
        } else {
            Drive.Stop();
        }
    }
}
