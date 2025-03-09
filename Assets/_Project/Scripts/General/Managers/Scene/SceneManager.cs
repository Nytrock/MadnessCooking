using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private AudioSource _music;

    private const int MENU_ID = 0;
    private const int GAME_ID = 1;

    private void Awake() {
        _loadingScreen.SetActive(false);
    }

    public void LoadMenu() {
        StartCoroutine(LoadScene(MENU_ID));
    }

    public void LoadGame() {
        StartCoroutine(LoadScene(GAME_ID));
    }

    private IEnumerator LoadScene(int sceneId) {
        _loadingScreen.SetActive(true);
        _music.Stop();
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneId);

        while (!asyncLoad.isDone)
            yield return null;
    }

    public static bool IsMenu() {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == MENU_ID;
    }

    public static bool IsGame() {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == GAME_ID;
    }
}
