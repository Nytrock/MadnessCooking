using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Fountain : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider SliderFountain;
    public bool isHold;
    public Text CountText;
    public int Count;
    public GameObject Icon;
    public bool isMilk;
    public Fridge fridge;
    public Upgrade NeedUpgrade;
    public Farm farm;
    public bool Auto = false;
    public AudioSource Hold;
    public Transform Camera;
    public Animator animator;
    public Education education;

    void Start()
    {
        UpdateUpgrade();
    }

    void Update()
    {

        if (isHold || Auto)  {
            if (isHold) {
                SliderFountain.gameObject.SetActive(true);
                Icon.SetActive(true);
            } else {
                SliderFountain.gameObject.SetActive(false);
                Icon.SetActive(false);
            }

            if (!isMilk) { 
                if (!Hold.isPlaying && Camera.position.x == -36.2f && !animator.GetBool("Active")) { 
                    Hold.Play();
                } else if (Camera.position.x != -36.2f || animator.GetBool("Active")) {
                    Hold.Stop();
                }
            } else {
                if (!Hold.isPlaying && animator.GetBool("Active")) { 
                    Hold.Play();
                } else if (!animator.GetBool("Active")) {
                    Hold.Stop();
                }
            }
            if (SliderFountain.value < SliderFountain.maxValue) {
                SliderFountain.value += Time.deltaTime;
            } else {
                SliderFountain.value = 0;
                Count += 1;
                CountText.text = Count.ToString();
                education.AddIndex(false);
                if (isMilk) {
                    fridge.CountMilk += 1;
                    if (farm.AvailibleUpgrades.Contains(fridge.AutoUpgrade))
                        fridge.MilkToCar();
                }
                CountText.text = Count.ToString();
            }
        } else {
            SliderFountain.gameObject.SetActive(false);
            Icon.SetActive(false);
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

    public void UpdateUpgrade() {
        if (!isMilk) {
            if (farm.AvailibleUpgrades.Contains(NeedUpgrade)) {
                Auto = true;
                SliderFountain.maxValue = 4;
            }
        } else {
            if (farm.AvailibleUpgrades.Contains(NeedUpgrade)) {
                Auto = true;
                SliderFountain.maxValue = 6;
            }
        }
    }
}

