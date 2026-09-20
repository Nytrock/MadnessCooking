using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    [RequireComponent(typeof(Button))]
    public class InternetShopOpener : MonoBehaviour {
        [SerializeField] private InternetPageManager _pageManager;
        [SerializeField] private InternetShopPage _shopPage;

        private void Awake() {
            var button = GetComponent<Button>();
            button.onClick.AddListener(delegate { _pageManager.ChangePage(_shopPage); });
        }
    }
}
