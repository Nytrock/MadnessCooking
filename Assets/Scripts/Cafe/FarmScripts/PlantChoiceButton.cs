using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantChoiceButton : MonoBehaviour
{
    public OkChoicePlant ButtonOk;
    public Sprite ActiveBorder;
    public Sprite NoActiveBorder;
    public Ingridient MyIngridient;
    public Image IngridientSprite;
    private Image sp;
    public AudioSource Press;

    void Start()
    {
        sp = this.GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonOk.ChoisedIngridient == MyIngridient)
            sp.sprite = ActiveBorder;
        else
            sp.sprite = NoActiveBorder;
    }

    public void SetActiveIngridient()
    {
        Press.Play();
        if (ButtonOk.ChoisedIngridient == null || ButtonOk.ChoisedIngridient != MyIngridient)
            ButtonOk.ChoisedIngridient = MyIngridient;
        else
            ButtonOk.ChoisedIngridient = null;
    }
}
