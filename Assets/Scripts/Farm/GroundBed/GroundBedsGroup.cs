using UnityEngine;

public class GroundBedsGroup : MonoBehaviour
{
    [SerializeField] private float _size;
    [SerializeField] private GroundBed[] _groundBeds;

    public float GetBedSize()
    {
        return _size;
    }

    public void SetBedsUI(IngredientChoiceUI ingredientChoiceUI, BedChoiceUI bedChoiceUI)
    {
        foreach (var bed in _groundBeds) {
            bed.SetUI(ingredientChoiceUI);
            bed.GetComponent<BedChoice>().SetUI(bedChoiceUI);
        }
    }
}
