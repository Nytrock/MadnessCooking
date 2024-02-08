using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BedTypeHolderUI : MonoBehaviour
{
    [SerializeField] private GameObject _UI;
    [SerializeField] private ItemInfoShower _shower;

    [Header("Side buttons")]
    [SerializeField] private bool _isSideButtonsWork;
    [SerializeField] private Button _waterButton;
    [SerializeField] private Button _fertilizeButton;

    public bool IsSideButtonsWork => _isSideButtonsWork;

    private void Start()
    {
        _UI.SetActive(false);
    }

    public void ChangeMode()
    {
        _UI.SetActive(!_UI.activeSelf);
    }

    public void UpdateIngredient(Ingredient ingredient)
    {
        _shower.SetItemInfo(ingredient);
        _shower.SetCount("0");
    }

    public void CheckWater(int count)
    {
        if (!_isSideButtonsWork)
            return;

        _waterButton.interactable = count > 0;
    }

    public void CheckFertilize(int count)
    {
        if (!_isSideButtonsWork)
            return;

        _fertilizeButton.interactable = count > 0;
    }

    public void UpdateCount(int count)
    {
        _shower.SetCount(count.ToString());
    }
}
