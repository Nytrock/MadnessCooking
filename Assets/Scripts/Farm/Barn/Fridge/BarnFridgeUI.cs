using UnityEngine;

public class BarnFridgeUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private BarnFridge _barnFridge;
    [SerializeField] private ItemInfoShowers _milkShower;
    [SerializeField] private ItemInfoShowers _flourShower;


    [SerializeField] private Ingredient _milk;
    [SerializeField] private Ingredient _flour;

    private void Start()
    {
        _panel.SetActive(false);

        _milkShower.SetItemInfo(_milk);
        _flourShower.SetItemInfo(_flour);

        _barnFridge.MilkChanged += UpdateMilkCount;
        _barnFridge.FlourChanged += UpdateFlourCount;
    }

    public void ChangeState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    private void UpdateMilkCount(int count)
    {
        _milkShower.SetCount(count);
    }

    private void UpdateFlourCount(int count)
    {
        _flourShower.SetCount(count);
    }
}
