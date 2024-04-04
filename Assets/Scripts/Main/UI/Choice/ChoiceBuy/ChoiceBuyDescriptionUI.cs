using UnityEngine;

public class ChoiceBuyDescriptionUI : MonoBehaviour
{
    [SerializeField] private ItemInfoRendererWithNum _renderer;
    [SerializeField] private string _freeText;

    public void UpdateDescription(BuyableObject item)
    {
        _renderer.SetItemInfo(item);
        if (item.Cost > 0)
            _renderer.SetNumText(item.Cost.ToString());
        else
            _renderer.SetNumText(_freeText);
    }

    public void ChangeActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (!gameObject.activeSelf)
            _renderer.SetNumText("");
    }

    public void ChangeActive(bool newvalue)
    {
        gameObject.SetActive(newvalue);
        if (!gameObject.activeSelf)
            _renderer.SetNumText("");
    }
}
