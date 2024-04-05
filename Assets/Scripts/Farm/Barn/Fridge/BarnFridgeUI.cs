using UnityEngine;

public class BarnFridgeUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private BarnFridge _barnFridge;
    [SerializeField] private ItemInfoRendererWithCount _milkRenderer;
    [SerializeField] private ItemInfoRendererWithCount _flourRenderer;

    [SerializeField] private Ingredient _milk;
    [SerializeField] private Ingredient _flour;

    private void Start()
    {
        _panel.SetActive(false);

        _milkRenderer.SetItemInfo(_milk);
        _flourRenderer.SetItemInfo(_flour);

        _barnFridge.MilkChanged += UpdateMilkCount;
        _barnFridge.FlourChanged += UpdateFlourCount;
    }

    public void ChangeState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    private void UpdateMilkCount(int count)
    {
        _milkRenderer.SetCount(count);
    }

    private void UpdateFlourCount(int count)
    {
        _flourRenderer.SetCount(count);
    }
}
