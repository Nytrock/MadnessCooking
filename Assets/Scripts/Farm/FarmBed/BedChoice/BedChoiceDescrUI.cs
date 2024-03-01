using UnityEngine;

public class BedChoiceDescrUI : MonoBehaviour
{
    [SerializeField] private ItemInfoRendererWithNum _renderer;

    public void UpdateDescription(BedType bedType)
    {
        _renderer.SetItemInfo(bedType);
        if (bedType.Cost > 0)
            _renderer.SetNumText(bedType.Cost.ToString());
        else
            _renderer.SetNumText("Free");
    }

    public void ChangeActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
