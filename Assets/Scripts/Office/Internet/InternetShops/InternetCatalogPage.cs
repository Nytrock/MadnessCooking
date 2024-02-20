using UnityEngine;

public class InternetCatalogPage : MonoBehaviour
{
    [SerializeField] private BaseBuyPanel _buyPanelPrefab;
    [SerializeField, Min(1)] private int _maxItemCount;

    public int MaxItemCount => _maxItemCount;

    public void GeneratePanel(BuyableObject item)
    {
        var buyPanel = Instantiate(_buyPanelPrefab, transform);
        buyPanel.Setup(item);
    }

    public void ChangeState(bool newValue)
    {
        gameObject.SetActive(newValue);
    }
}
