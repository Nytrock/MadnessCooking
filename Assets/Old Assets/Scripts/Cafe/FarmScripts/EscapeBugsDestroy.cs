using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBugsDestroy : MonoBehaviour
{
    public Animator PanelDestroy;
    public GameObject ButtonMenu;
    public AudioSource Press;

    public void NoActiveDestroy()
    {
        Press.Play();
        PanelDestroy.SetBool("Active", false);
        ButtonMenu.SetActive(true);
    }
}
