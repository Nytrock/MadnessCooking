using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManureCreate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isHold;
    public GameObject Panel;
    public float MaxTimeShit;
    public float CurrentTimeShit;
    public Slider slider;
    public int CountManure;
    public Text TextManure;
    public int CountShit;
    public Text TextShit;
    public Upgrade NeedUpgrade;
    public Farm farm;
    public bool Auto = false;
    public AudioSource Hold;
    public Animator animator;
    public Education education;

    void Start()
    {
        UpdateUpgrade();
    }

    void Update()
    {
        TextShit.text = CountShit.ToString();
        TextManure.text = CountManure.ToString();
        if (isHold || Auto) {
            if (isHold)
                Panel.SetActive(true);
            else
                Panel.SetActive(false);
            if (CountShit > 0) {
                if (!Hold.isPlaying && animator.GetBool("Active")) {
                    Hold.Play();
                } else if (!animator.GetBool("Active")) {
                    Hold.Stop();
                }
                if (slider.value < slider.maxValue) {
                    slider.value += Time.deltaTime;
                } else {
                    slider.value = 0;
                    if (NewOrContinue.Education)
                        education.AddIndex(false);
                    CountManure += 1;
                    CountShit -= 1;
                }
            } else {
                Hold.Stop();
            }
            if (Auto) {
                if (CurrentTimeShit < MaxTimeShit) {
                    CurrentTimeShit += Time.deltaTime;
                } else {
                    CurrentTimeShit = 0;
                    CountShit += 1;
                }
            }
        } else {
            Hold.Stop();
            Panel.SetActive(false);
            if (CurrentTimeShit < MaxTimeShit) {
                CurrentTimeShit += Time.deltaTime;
            } else {
                CurrentTimeShit = 0;
                CountShit += 1;
            }
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
        if (farm.AvailibleUpgrades.Contains(NeedUpgrade)){
            Auto = true;
            MaxTimeShit = 15;
        }
    }
}
