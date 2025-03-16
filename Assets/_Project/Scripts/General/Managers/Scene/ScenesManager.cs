using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);

        while (!asyncLoad.isDone)
            yield return null;
    }

    public static bool IsMenu() {
        return SceneManager.GetActiveScene().buildIndex == MENU_ID;
    }

    public static bool IsGame() {
        return SceneManager.GetActiveScene().buildIndex == GAME_ID;
    }
}
