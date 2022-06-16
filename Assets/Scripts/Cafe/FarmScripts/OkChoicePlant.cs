using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkChoicePlant : MonoBehaviour
{
    public EscChoicePlant esc;
    public Ingridient ChoisedIngridient;
    public GroundBed ground;
    private Button button;
    public AudioSource Press;

    void Start()
    {
        button = this.GetComponent<Button>();
    }

    void Update()
    {
        if (ChoisedIngridient != null)
            button.interactable = true;
        else
            button.interactable = false;
    }

    public void OkChoiceIngridient()
    {
        Press.Play();
        ground.plant.sprite = ChoisedIngridient.ImagePlant;
        ground.plant.color = new Color(1f, 1f, 1f, 1f);
        ground.ingridient = ChoisedIngridient;
        ground.SetInterface(ChoisedIngridient);
        ground.ButtonsMenu.SetActive(true);
        esc.PanelActive();
    }
}
