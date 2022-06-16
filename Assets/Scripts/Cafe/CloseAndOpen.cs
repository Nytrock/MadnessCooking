using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseAndOpen : MonoBehaviour
{
    public bool Close;
    public Text TextClose;
    public Cafe cafe;
    public AudioSource Change;

    public void Pressed(){
        Change.Play();
        if (Close) {
            TextClose.text = "Open";
        } else {
            TextClose.text = "Close";
            cafe.Closed();
        }
        Close = !Close;
    }
}
