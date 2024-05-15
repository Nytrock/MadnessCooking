using UnityEngine;

public class KitchenStorageChanger : DecorHolder
{
    [SerializeField] private GameObject _boxStorage;
    [SerializeField] private GameObject _fridgeStorage;

    public override void ChangeState(bool newValue)
    {
        _boxStorage.SetActive(!newValue);
        _fridgeStorage.SetActive(newValue);
    }
}
