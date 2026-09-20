using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class InternetPageManager : MonoBehaviour {
        [SerializeField] private InternetHomePage _homePage;
        [SerializeField] private InternetDownload _download;

        [Header("Address")]
        [SerializeField] private LocalizedText _address;
        [SerializeField] private string _defaultAddress = "Internet.Address";
        [SerializeField] private string _searchAddress = "Internet.SearchAddress";

        private InternetPage _nowPage;

        public event Action<InternetPage> PageChanged;

        private void Start() {
            OpenHomePage();
        }

        public void OpenHomePage() {
            _download.StopDownload();
            ChangePage(_homePage);
        }

        public void ChangePage(InternetPage page) {
            if (_nowPage != null)
                _nowPage.ChangeState(false);
            _nowPage = page;

            if (_nowPage == _homePage)
                _nowPage.ChangeState(true);
            else
                _download.StartDownload(_nowPage);

            PageChanged?.Invoke(_nowPage);
            UpdateAddress();
        }

        private void UpdateAddress() {
            _address.ClearArguments();
            _address.AddArguments("pageTitle", _nowPage.PageName);

            if (_nowPage as InternetSearchPage)
                _address.SetText(_searchAddress);
            else
                _address.SetText(_defaultAddress);
        }
    }
}
