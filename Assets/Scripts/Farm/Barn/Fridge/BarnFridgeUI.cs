using UnityEngine;

public class BarnFridgeUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private BarnFridge _barnFridge;
    [SerializeField] private ItemInfoRendererWithCount _milkRenderer;
    [SerializeField] private ItemInfoRendererWithCount _flourRenderer;

    private void Start()
    {
        _panel.SetActive(false);
        _milkRenderer.SetItemInfo(IngredientsManager.Instance.Milk);
        _flourRenderer.SetItemInfo(IngredientsManager.Instance.Flour);
    }

    private void Update()
    {
        _milkRenderer.SetCount(_barnFridge.Cow.ReadyCount);
        _flourRenderer.SetCount(_barnFridge.FlourMill.ReadyCount);
    }

    public void ChangeState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }
}
