using UnityEngine;

public class ShopCatalogPageUpper : MonoBehaviour {
    [SerializeField] private GameObject[] _uppers;

    private void Awake() {
        foreach (var upper in _uppers)
            upper.SetActive(false);
    }

    public void ActivateUpper(int index) {
        if (index < 0 || index >= _uppers.Length)
            return;

        _uppers[index].SetActive(true);
    }

    public void DisableUpper(int panelsCount) {
        if (panelsCount >= _uppers.Length)
            return;

        _uppers[panelsCount].SetActive(false);
    }
}
