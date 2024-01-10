using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Internet : MonoBehaviour
{
    public Animator PanelOrders;
    public Animator PanelIngridients;
    public Animator PanelFood;
    public Animator PanelTechnic;
    public Animator PanelUpgrades;
    public GameObject Escape;
    public GameObject Buttons;
    public Loading loading;
    public AudioSource Work;
    public AudioSource Start;
    public AudioSource Press;
    public AudioSource Exit;

    public void ActivePanel()
    {
        PanelOrders.SetBool("Active", !PanelOrders.GetBool("Active"));
        if (PanelOrders.GetBool("Active")) { 
            loading.ActivateLoading();
            Start.Play();
            Work.Play();
        } else {
            Start.Stop();
            Work.Stop();
            Exit.Play();
        }
        Buttons.SetActive(!Buttons.activeSelf);
    }

    public void ActiveIngridientsShop()
    {
        PanelIngridients.SetBool("Active", !PanelIngridients.GetBool("Active"));
        if (PanelIngridients.GetBool("Active"))
            loading.ActivateLoading();
        Escape.SetActive(!Escape.activeSelf);
        Press.Play();
    }

    public void ActiveFoodShop()
    {
        PanelFood.SetBool("Active", !PanelFood.GetBool("Active"));
        if (PanelFood.GetBool("Active"))
            loading.ActivateLoading();
        Escape.SetActive(!Escape.activeSelf);
        Press.Play();
    }

    public void ActiveTechnicShop()
    {
        PanelTechnic.SetBool("Active", !PanelTechnic.GetBool("Active"));
        if (PanelTechnic.GetBool("Active"))
            loading.ActivateLoading();
        Escape.SetActive(!Escape.activeSelf);
        Press.Play();
    }

    public void ActiveUpgradesShop()
    {
        PanelUpgrades.SetBool("Active", !PanelUpgrades.GetBool("Active"));
        if (PanelUpgrades.GetBool("Active"))
            loading.ActivateLoading();
        Escape.SetActive(!Escape.activeSelf);
        Press.Play();
    }
}
