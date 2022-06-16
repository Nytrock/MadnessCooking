using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public List<Ingridient> AvailibleIngridients;
    public List<Upgrade> AvailibleUpgrades;
    public Transform container;
    public PlantChoiceButton PlantChoice;
    public OkChoicePlant ok;
    public List<GroundBed> GroundBeds;

    public void FillChoicePlant() {
        foreach (Transform obj in container)
            Destroy(obj.gameObject);
        foreach(Ingridient ingridient in AvailibleIngridients) {
            var ing = Instantiate(PlantChoice, container);
            ing.IngridientSprite.sprite = ingridient.ImageIngridient;
            ing.MyIngridient = ingridient;
            ing.ButtonOk = ok;
        }
    }
}
