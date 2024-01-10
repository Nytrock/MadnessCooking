using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public float MaxFloat;
    public float MinFloat;
    public float MinTime;
    public float MaxTime;
    public bool Activate;
    public Slider slider;
    public AudioSource Load;
    public AudioSource Fin;

    void Update()
    {
        if (Activate) {
            if (slider.value < slider.maxValue){
                slider.value += Time.deltaTime * Random.Range(MinTime, MaxTime);
            } else {
                this.GetComponent<Animator>().SetBool("Active", false);
                Activate = false;
                Load.Stop();
                Fin.Play();
            }
        }
    }

    public void ActivateLoading()
    {
        Activate = true;
        this.GetComponent<Animator>().SetBool("Active", true);
        slider.maxValue = Random.Range(MaxFloat, MinFloat);
        slider.value = 0;
        Load.Play();
    }
}
