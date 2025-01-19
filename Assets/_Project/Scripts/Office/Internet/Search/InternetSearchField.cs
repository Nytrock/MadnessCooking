using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class InternetSearchField : MonoBehaviour {
    [SerializeField] private InternetPageManager _pageManager;
    [SerializeField] private InternetSearchManager _searchManager;

    private TMP_InputField _inputField;

    private void Awake() {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onSubmit.AddListener(_searchManager.Search);

        _searchManager.NewSearch += ChangeInput;
        _pageManager.PageChanged += delegate { ChangeInput(string.Empty); };
        ChangeInput(_searchManager.NowQuery);
    }

    private void ChangeInput(string newInput) {
        _inputField.text = newInput;
    }

    public void Searh() {
        _searchManager.Search(_inputField.text);
    }
}
