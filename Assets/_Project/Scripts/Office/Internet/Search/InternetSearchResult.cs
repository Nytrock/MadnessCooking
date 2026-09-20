using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class InternetSearchResult : MonoBehaviour {
        [SerializeField] private string[] _keywords;

        public void ChangeState(bool newState) {
            gameObject.SetActive(newState);
        }

        public bool QueryContainKeywords(string query) {
            foreach (var keyword in _keywords)
                if (query.ToLower() == keyword.ToLower())
                    return true;
            return false;
        }
    }
}
