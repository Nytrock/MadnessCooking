using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarTimer : MonoBehaviour
{
    public FarmCar Car;
    public Text TextTime;
    public float AllTime;
    public float NowTime;
    public GameObject Timer;
    public Farm farm;
    private int min;
    private int sec;
    public List<Upgrade> TimerUpgrades;
    public List<Upgrade> CarUpgrades;

    void Start()
    {
        UpdateSpeed();
    }

    public void Update() {
        if (NowTime > 0) {
            Timer.gameObject.SetActive(true);
            TextTime.text = ((int)NowTime / 60).ToString("00") + ":" + ((int)NowTime - ((int)NowTime) / 60 * 60).ToString("00");
            NowTime -= Time.deltaTime;
        } else {
            Timer.gameObject.SetActive(false);
            Car.Send.interactable = true;
        }
    }

    public void UpdateSpeed()
    {
        if (farm.AvailibleUpgrades.Contains(TimerUpgrades[3]))
            AllTime = 60f;
        else if (farm.AvailibleUpgrades.Contains(TimerUpgrades[2]))
            AllTime = 180f;
        else if (farm.AvailibleUpgrades.Contains(TimerUpgrades[1]))
            AllTime = 240f;
        else if (farm.AvailibleUpgrades.Contains(TimerUpgrades[0]))
            AllTime = 300f;
        else
            AllTime = 360f;

        if (farm.AvailibleUpgrades.Contains(CarUpgrades[3])) {
            Car.Size = -1;
            Car.TextFullness.gameObject.SetActive(false);
        } else if (farm.AvailibleUpgrades.Contains(CarUpgrades[2])) {
            Car.Size = 2000;
        } else if (farm.AvailibleUpgrades.Contains(CarUpgrades[1])) {
            Car.Size = 1000;
        } else if (farm.AvailibleUpgrades.Contains(CarUpgrades[0])) {
            Car.Size = 500;
        } else {
            Car.Size = 200;
        }
        Car.TextFullness.text = Car.Fullness.ToString() + "/" + Car.Size.ToString();

        if (NowTime > 0)
            Car.Send.interactable = false;
        else
            Car.Send.interactable = true;
    }
}
