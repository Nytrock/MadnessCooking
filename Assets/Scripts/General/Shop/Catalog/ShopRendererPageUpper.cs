using UnityEngine;

public class ShopRendererPageUpper : MonoBehaviour {
    [SerializeField] private GameObject[] _uppers;

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
