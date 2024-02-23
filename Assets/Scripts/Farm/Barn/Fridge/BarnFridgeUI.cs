using UnityEngine;

public class BarnFridgeUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private BarnFridge _barnFridge;
    [SerializeField] private ItemInfoRendererWithNum _milkRenderer;
    [SerializeField] private ItemInfoRendererWithNum _flourRenderer;

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
        _milkRenderer.SetNumText(count.ToString());
    }

    private void UpdateFlourCount(int count)
    {
        _flourRenderer.SetNumText(count.ToString());
    }
}
