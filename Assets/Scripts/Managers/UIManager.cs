using UnityEngine;

public class UIManager : MonoBehaviour {
    static public UIManager instance;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _nextLevelButton;

    void Awake() {
        if (instance && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public void pauseGame() {
        Time.timeScale = 0.0f;
        _pausePanel.SetActive(true);
        _pauseButton.SetActive(false);
    }

    public void resumeGame() {
        _pausePanel.SetActive(false);
        _pauseButton.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void nextLevel() {
        GameManager.instance.loadNextLevel();
    }

    private void loadLevelCompletePanel() {
        _levelCompletePanel.SetActive(true);
        _nextLevelButton.SetActive(
            GameManager.instance.win || 
            LevelDataHandler.getLevelState(GameManager.instance.levelID + 1)
        );
    }

    public void levelComplete() {
        Invoke(nameof(loadLevelCompletePanel), 3.0f);
    }
}
