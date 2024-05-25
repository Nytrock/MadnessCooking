using UnityEngine;

public class ChoiceBuyDescriptionUI : MonoBehaviour
{
    [SerializeField] private ItemInfoRendererWithCost _renderer;
    [SerializeField] private string _buyDescription;
    [SerializeField] private string _freeDescription;

    public void UpdateDescription(BuyableObject item)
    {
        _renderer.SetItemInfo(item);
        if (item.Cost > 0)
            _renderer.SetCost(_buyDescription, item.Cost);
        else
            _renderer.SetCost(_freeDescription);
    }

    public void ChangeActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (!gameObject.activeSelf)
            _renderer.SetCost("");
    }

    public void ChangeActive(bool newvalue)
    {
        gameObject.SetActive(newvalue);
        if (!gameObject.activeSelf)
            _renderer.SetCost("");
    }
}
