using UnityEngine;

public abstract class BaseInternetShop : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private InternetCatalog _catalog;
    [SerializeField] private InternetCatalogPage _pagePrefab;

    protected BuyableObject[] _itemsToBuy;

    private void Start()
    {
        GenerateShop();
        ChangeShopState(false);
    }

    public void ChangeShopState(bool newState)
    {
        _shop.SetActive(newState);
    }

    protected void GenerateShop() {
        SetObjectsArray();

        int pageCount = _pagePrefab.MaxItemCount;
        int pageNum = _itemsToBuy.Length / pageCount;
        if (_itemsToBuy.Length % pageCount != 0)
            pageNum++;

        for (int i = 0; i < pageNum; i++) {
            var page = Instantiate(_pagePrefab, _catalog.PagesContainer);
            for (int j = 0; j < pageCount; j++) {
                if (i * pageCount + j >= _itemsToBuy.Length)
                    break;

                page.GeneratePanel(_itemsToBuy[i * pageCount + j]);
            }
            _catalog.AddPage(page);
        }

        _catalog.ActivateFirstPage();
    }

    protected abstract void SetObjectsArray();
    protected abstract void BuyItem(int itemIndex);
}
