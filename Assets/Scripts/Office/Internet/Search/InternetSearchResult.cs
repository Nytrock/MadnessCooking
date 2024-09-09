using UnityEngine;

public class InternetSearchResult : MonoBehaviour {
    [SerializeField] private string[] _keywords;

    public void ChangeState(bool newState) {
        gameObject.SetActive(newState);
    }

    public bool QueryContainKeywords(string query) {
        query = query.ToLower();
        foreach (var keyword in _keywords)
            if (query == keyword)
                return true;
        return false;
    }
}
