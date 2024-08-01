using TMPro;
using UnityEngine;

public class InternetSearchManager : MonoBehaviour {
    [SerializeField] private InternetPageManager _pageManager;
    [SerializeField] private InternetSearchPage _searchPage;
    [SerializeField] private TMP_InputField _inputField;

    [Header("Results")]
    [SerializeField] private InternetSearchResult _defaultResult;
    [SerializeField] private InternetSearchResult[] _results;

    private string _nowQuery;

    private void Awake() {
        _pageManager.PageChanged += CheckOpenedPage;
        _inputField.onSubmit.AddListener(Search);
    }

    private void CheckOpenedPage(InternetPage page) {
        if (page as InternetSearchPage)
            return;

        HideAllResults();
    }

    private void Start() {
        HideAllResults();
    }

    public void Searh() {
        Search(_inputField.text);
    }

    public void Search(string query) {
        _inputField.text = "";
        if (query.Replace(" ", "") == "" || _nowQuery == query)
            return;
        _nowQuery = query;

        _searchPage.UpdateName(query);
        _pageManager.ChangePage(_searchPage);
        CheckResults();
    }

    private void CheckResults() {
        HideAllResults();

        foreach (var result in _results) {
            if (result.QueryContainKeywords(_nowQuery)) {
                result.ChangeState(true);
                return;
            }
        }

        _defaultResult.ChangeState(true);
    }

    private void HideAllResults() {
        foreach (var result in _results)
            result.ChangeState(false);
        _defaultResult.ChangeState(false);
    }
}
