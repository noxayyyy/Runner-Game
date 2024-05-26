using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour {
    static public void loadScene(string scene) {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(scene);
    }

    static public void loadSceneAsync(string scene) {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(scene);
    }

    static public void reloadScene() {
        loadScene(SceneManager.GetActiveScene().name);
    }

    static public void goToSplash() {
        loadScene(Constants.SCENE_SPLASH);
    }

    static public void goToMainMenu() {
        loadScene(Constants.SCENE_MAIN_MENU);
    }

    static public void goToLevelSelect() {
        loadScene(Constants.SCENE_LEVEL_SELECT);
    }

    static public void goToGameplay() {
        loadScene(Constants.SCENE_GAMEPLAY);
    }

    static public void goToRating() {
        Debug.Log("Rate Us!");
    }

    static public void goToPrivacy() {
        Debug.Log("Privacy Policy");
    }

    static public void goToMoreGames() {
        Debug.Log("More Games");
    }
}
