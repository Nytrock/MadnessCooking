using TMPro;
using UnityEngine;

public class InternetSearchPage : InternetPage {
    [SerializeField] private InternetSearchManager _searchManager;
    [SerializeField] private TMP_InputField _searchInput;

    private void Awake() {
        _searchInput.onSubmit.AddListener(_searchManager.Search);
    }

    public void Search() {
        _searchManager.Search(_searchInput.text);
    }

    public void UpdateName(string name) {
        _pageName = name;
        _searchInput.text = name;
    }
}
