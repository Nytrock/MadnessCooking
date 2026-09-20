using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class InternetSearchManager : MonoBehaviour {
        [SerializeField] private InternetPageManager _pageManager;
        [SerializeField] private InternetSearchPage _searchPage;

        [Header("Results")]
        [SerializeField] private InternetSearchResult _defaultResult;
        [SerializeField] private InternetSearchResult[] _results;

        private string _nowQuery;

        public event Action<string> NewSearch;

        public string NowQuery => _nowQuery;

        private void Awake() {
            _pageManager.PageChanged += CheckOpenedPage;
            _searchPage.PageLoaded += CheckResults;
        }

        private void CheckOpenedPage(InternetPage page) {
            if (page as InternetSearchPage)
                return;

            HideAllResults();
        }

        private void Start() {
            HideAllResults();
        }

        public void Search(string query) {
            HideAllResults();
            if (query.Replace(" ", "") == "" || _nowQuery == query)
                return;
            _nowQuery = query;

            _searchPage.UpdateName(query);
            _pageManager.ChangePage(_searchPage);
            NewSearch?.Invoke(query);
        }

        private void CheckResults() {
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
}
