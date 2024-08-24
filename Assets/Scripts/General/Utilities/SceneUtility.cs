using UnityEngine.SceneManagement;

public static class SceneUtility {
    private const int MENU_ID = 0;
    private const int GAME_ID = 1;

    public static void LoadMenu() {
        SceneManager.LoadScene(MENU_ID);
    }

    public static void LoadGame() {
        SceneManager.LoadScene(GAME_ID);
    }

    public static bool IsMenu() {
        return SceneManager.GetActiveScene().buildIndex == MENU_ID;
    }

    public static bool IsGame() {
        return SceneManager.GetActiveScene().buildIndex == GAME_ID;
    }
}
