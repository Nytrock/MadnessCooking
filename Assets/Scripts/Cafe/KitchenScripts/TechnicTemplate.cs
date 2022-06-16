using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnicTemplate : MonoBehaviour
{
    public Technic technic;
    public bool Starting;
    public GameObject DescriptionTemplate;
    public TextIngridient DescriptionTechnic;
    public Order DescriptionFood;
    public bool StrenthBool;
    public int strength;
    public int MaxStregth;
    public Button Repair;
    public Slider SliderRepair;
    public bool Repairing;
    public MoneyBar money;
    public Text CostRepairText;
    public float FasterSpeed;
    public AudioSource Open;
    public AudioSource Work;
    public AudioSource RepairVolume;
    public Transform Camera;
    public ForOrders forOrders;

    void Start()
    {
        DescriptionTechnic.MainText.text = technic.Name;
        DescriptionTechnic.DescriptionText.text = technic.Description;
        DescriptionTechnic.Description.sprite = technic.MiniIcon;
        CostRepairText.text = technic.CostRepairing.ToString();
    }

    void Update() {
        if (!Repairing) { 
            SliderRepair.gameObject.SetActive(false);
            if (Starting){
                Repair.interactable = false;
                DescriptionTechnic.gameObject.SetActive(false);
                DescriptionFood.gameObject.SetActive(true);
                if (DescriptionFood.slider.value < DescriptionFood.slider.maxValue) {
                    DescriptionFood.Final.SetActive(false);
                    DescriptionFood.slider.value += Time.deltaTime * FasterSpeed;
                    if (Camera.position.x != -18.1f)
                        Work.Stop();
                    else if (!Work.isPlaying)
                        Work.Play();
                } else{
                    DescriptionFood.Final.SetActive(true);
                    Work.Stop();
                    this.GetComponent<Image>().sprite = technic.Icon;
                    if (StrenthBool) {
                        strength -= (int)(Random.Range(1f, MaxStregth * 0.08f));
                        StrenthBool = false;
                    }
                    Starting = false;
                    forOrders.UpgradeIngridientsOrders();
                }
            } else {
                if (strength < technic.Strength && technic.CostRepairing <= money.Coins)
                    Repair.interactable = true;
                else
                    Repair.interactable = false;
                DescriptionTechnic.gameObject.SetActive(true);
                DescriptionFood.gameObject.SetActive(false);
                StrenthBool = true;
            }
        } else {
            if (SliderRepair.value < SliderRepair.maxValue) { 
                SliderRepair.value += Time.deltaTime;
                if (Camera.position.x != -18.1f)
                    RepairVolume.Stop();
                else if (!RepairVolume.isPlaying)
                    RepairVolume.Play();
            } else {
                strength = technic.Strength;
                RepairVolume.Stop();
                Repair.gameObject.SetActive(true);
                Repairing = false;
                forOrders.UpgradeIngridientsOrders();
            }
        }
    }

    public void Activate()
    {
        DescriptionTemplate.SetActive(!DescriptionTemplate.activeSelf);
        Open.Play();
    }

    public void RepairTechnic()
    {
        money.AddCoins(-technic.CostRepairing);
        Repairing = true;
        SliderRepair.gameObject.SetActive(true);
        Repair.gameObject.SetActive(false);
        SliderRepair.value = 0;
        SliderRepair.maxValue = technic.TimeRepairing;
        RepairVolume.Play();
        forOrders.UpgradeIngridientsOrders();
    }
}
