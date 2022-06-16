using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fridge : MonoBehaviour
{
    public int CountMilk;
    public int CountFlour;
    public Text TextMilk;
    public Text TextFlour;
    public GameObject Panel;
    public FarmCar Car;
    public Ingridient Milk;
    public Ingridient Flour;
    public Fountain fountain;
    public FlourCreate flourCreate;
    public Upgrade AutoUpgrade;
    public Farm farm;
    public AudioSource Open;
    public AudioSource Close;
    public AudioSource GiveMilk;
    public AudioSource GiveFlour;

    public void ActivePanel()
    {
        Panel.SetActive(!Panel.activeSelf);
        if (Panel.activeSelf)
            Open.Play();
        else
            Close.Play();
    }

    void Update()
    {
        TextFlour.text = CountFlour.ToString();
        TextMilk.text = CountMilk.ToString();
    }

    public void MilkToCar() {
        if (CountMilk > 0) {
            GiveMilk.Play();
            if (Car.IngridientsInCar.Contains(Milk)) { 
                if (Car.Fullness + CountMilk <= Car.Size || Car.Size == -1) { 
                    Car.CountIngridientsCar[Car.IngridientsInCar.IndexOf(Milk)] += CountMilk;
                    CountMilk -= Car.Fullness;
                    CountMilk = 0;
                } else {
                    Car.CountIngridientsCar[Car.IngridientsInCar.IndexOf(Milk)] += Car.Size - Car.Fullness;
                    CountMilk -= Car.Size - Car.Fullness;
                    Car.Fullness = Car.Size;
                }
            } else {
                Car.IngridientsInCar.Add(Milk);
                if (Car.Fullness + CountMilk <= Car.Size || Car.Size == -1) { 
                    Car.CountIngridientsCar.Add(CountMilk);
                    CountMilk -= Car.Fullness;
                    CountMilk = 0;
                } else {
                    Car.CountIngridientsCar.Add(Car.Size - Car.Fullness);
                    CountMilk -= Car.Size - Car.Fullness;
                    Car.Fullness = Car.Size;
                }
            }
            Car.UpdateIngridients();
            fountain.Count = 0;
            fountain.CountText.text = "0";
        }
    }

    public void FlourToCar()
    {
        if (CountFlour > 0) {
            GiveFlour.Play();
            if (Car.IngridientsInCar.Contains(Flour)) {
                if (Car.Fullness + CountFlour <= Car.Size || Car.Size == -1) { 
                    Car.CountIngridientsCar[Car.IngridientsInCar.IndexOf(Flour)] += CountFlour;
                    CountFlour -= Car.Fullness;
                    CountFlour = 0;
                } else {
                    Car.CountIngridientsCar[Car.IngridientsInCar.IndexOf(Flour)] += Car.Size - Car.Fullness;
                    CountFlour -= Car.Size - Car.Fullness;
                    Car.Fullness = Car.Size;
                }
            } else {
                Car.IngridientsInCar.Add(Flour);
                if (Car.Fullness + CountFlour <= Car.Size || Car.Size == -1) { 
                    Car.CountIngridientsCar.Add(CountFlour);
                    CountFlour -= Car.Fullness;
                    CountFlour = 0;
                } else {
                    Car.CountIngridientsCar.Add(Car.Size - Car.Fullness);
                    CountFlour -= Car.Size - Car.Fullness;
                    Car.Fullness = Car.Size;
                }
            }
            Car.UpdateIngridients();
        }
    }
}
