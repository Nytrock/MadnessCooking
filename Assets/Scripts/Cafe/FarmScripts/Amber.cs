using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amber : MonoBehaviour
{
    public Animator animator;
    public GameObject Buttons;
    public Shop shop;
    public AudioSource Open;
    public AudioSource fridge;
    public AudioSource chickens;

    public void OpenPanel()
    {
        animator.SetBool("Active", !animator.GetBool("Active"));
        Buttons.SetActive(!Buttons.activeSelf);
        shop.BuyUpgrade.SetActive(false);
        Open.Play();
    }

    public void ActiveVoice()
    {
        if (chickens.isPlaying) {
            chickens.Stop();
            fridge.Stop();
        } else {
            chickens.Play();
            fridge.Play();
        }
    }
}
