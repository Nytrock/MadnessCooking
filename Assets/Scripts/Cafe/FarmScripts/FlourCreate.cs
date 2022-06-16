using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlourCreate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Text TextFlour;
    public int CountCorn;
    public Text TextCorn;
    public bool isHold;
    public GameObject Panel;
    public Slider FlourSlider;
    public Fridge fridge;
    public GameObject Flour;
    public Upgrade NeedUpgrade;
    public Farm farm;
    public bool AutoFlour;
    public AudioSource Hold;
    public Animator animator;

    void Start()
    {
        UpdateUpgrade();
    }

    public void Update()
    {
        TextCorn.text = CountCorn.ToString();
        TextFlour.text = fridge.TextFlour.text;
        if ((isHold || AutoFlour) && CountCorn != 0) {
            Panel.SetActive(true);
            Flour.SetActive(true);
            if (!Hold.isPlaying && animator.GetBool("Active")) { 
                Hold.Play();
            } else if (!animator.GetBool("Active")) {
                Hold.Stop();
            }
            if (FlourSlider.value < FlourSlider.maxValue) {
                FlourSlider.value += Time.deltaTime;
            } else {
                FlourSlider.value = 0;
                CountCorn -= 1;
                fridge.CountFlour += 1;
                fridge.TextFlour.text = fridge.CountFlour.ToString();
                if (fridge.farm.AvailibleUpgrades.Contains(fridge.AutoUpgrade))
                    fridge.FlourToCar();
            }
        } else {
            Panel.SetActive(false);
            Flour.SetActive(false);
            Hold.Stop();
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isHold = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isHold = false;
    }

    public void UpdateUpgrade()
    {
        if (farm.AvailibleUpgrades.Contains(NeedUpgrade)) {
            AutoFlour = true;
        }
    }
}
