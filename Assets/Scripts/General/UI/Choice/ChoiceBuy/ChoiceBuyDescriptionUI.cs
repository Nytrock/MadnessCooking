using UnityEngine;

public class ChoiceBuyDescriptionUI : MonoBehaviour
{
    [SerializeField] private ItemInfoRendererWithPrice _renderer;
    [SerializeField] private string _buyDescription;
    [SerializeField] private string _freeDescription;

    public void UpdateDescription(BuyableObject item)
    {
        _renderer.SetItemInfo(item);
        if (item.Cost > 0)
            _renderer.SetPrice(_buyDescription, item.Cost);
        else
            _renderer.SetPrice(_freeDescription);
    }

    public void ChangeActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (!gameObject.activeSelf)
            _renderer.SetPrice("");
    }

    public void ChangeActive(bool newvalue)
    {
        gameObject.SetActive(newvalue);
        if (!gameObject.activeSelf)
            _renderer.SetPrice("");
    }
}
