using UnityEngine;

public class InternetPage : MonoBehaviour {
    [SerializeField] protected string _pageName;

    public string PageName => _pageName;

    private void Start() {
        ChangeState(false);
    }

    public virtual void ChangeState(bool newValue) {
        gameObject.SetActive(newValue);
    }
}
