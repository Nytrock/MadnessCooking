using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscChoicePlant : MonoBehaviour
{
    public Animator PanelChoice;
    public ScrollRect Scroll;
    public OkChoicePlant ButtonOk;
    public GameObject Buttons;
    public AudioSource Press;

    public void PanelActive()
    {
        Press.Play();
        PanelChoice.SetBool("Active", false);
        Buttons.SetActive(true);
        Scroll.enabled = true;
        ButtonOk.ground = null;
        ButtonOk.ChoisedIngridient = null;
    }
}
