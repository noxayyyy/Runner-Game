using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _levelButtonPrefab;

    void btnListener(int levelID) {
        DataPersistenceManager.save(Constants.PREF_LEVEL_NUMBER, levelID);
        SceneNavigator.goToGameplay();
    }

    void generateButton(int levelID, bool unlocked) {
        Vector3 pos = gameObject.transform.position;
        GameObject btn = Instantiate(
            _levelButtonPrefab, 
            pos, 
            Quaternion.identity, 
            gameObject.transform
        );
        btn.GetComponentInChildren<Text>().text = $"Level {levelID}";
        btn.GetComponent<Button>().interactable = unlocked;
        btn.GetComponent<Button>().onClick.AddListener(() => btnListener(levelID));
    }

    void Start() {
        for (int i = 1; i <= LevelDataHandler.getLevelCount(); i++) {
            generateButton(i, LevelDataHandler.getLevelState(i));
        }
    }
}
