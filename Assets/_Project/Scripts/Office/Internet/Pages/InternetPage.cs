using UnityEngine;

public class InternetPage : MonoBehaviour {
    [SerializeField] protected string _pageName;
    [SerializeField] protected GameObject _panel;

    public string PageName => _pageName;

    private void Start() {
        ChangeState(false);
    }

    public virtual void ChangeState(bool newValue) {
        _panel.SetActive(newValue);
    }
}
