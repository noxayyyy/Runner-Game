using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	static public GameManager instance;
	public bool win = false;
	private Player _player;
	private DressManager _dress;
	private LoadManager _loadManager;
	private LevelManager _level;
	private UIManager _ui;
	[SerializeField] private GameObject _startTrigger;
	[SerializeField] private GameObject[] _levelPrefabs;
	public int levelID {
		get;
		private set;
	}
	void Awake() {
		if (instance && instance != this) {
			Destroy(this);
			return;
		}
		instance = this;
		_loadManager = LoadManager.instance;
		_player = Player.instance;
		_dress = _player.GetComponent<DressManager>();
		levelID = DataPersistenceManager.loadInt(Constants.PREF_LEVEL_NUMBER);
	}

    public void loadNextLevel() {
        DataPersistenceManager.save(Constants.PREF_LEVEL_NUMBER, ++levelID);
        SceneNavigator.goToGameplay();
    }

	void Start() {
		Instantiate(
			_levelPrefabs[levelID - 1], 
			gameObject.transform.position,
			Quaternion.identity
		);
		_level = LevelManager.instance;
		_ui = UIManager.instance;
		_player.transform.position = _level.playerSpawn.position;
		_startTrigger.GetComponent<Text>().text = $"Level: {levelID}\nTap to start!";
		StartCoroutine(waitForStart());
	}

	public IEnumerator waitForStart() {
		if (_loadManager) {
			while (_loadManager.isLoading) {
				yield return null;
			}
		}
		_player.allowInput = true;
		while (!_player.hasInput) {
			yield return null;
		}
    	_startTrigger.SetActive(false);
		_player.startAutoMove();
    }

	public void pickupPart(PickupChoiceData.BodyPart part, int index, bool rightOrWrong) {
		_dress.changePart(part, index);
		// right or wrong logic
		if (rightOrWrong) {
			_level.points++;
		}
	}

    public IEnumerator endLevel() {
		win = (float)_level.points / _level.totalPoints * 100.0f >= _level.winThreshold;
        if (win) {
            LevelDataHandler.unlockLevel(levelID);
			Debug.Log("..............Win!");
        } else {
			Debug.Log("..............Fail!");
		}
		_player.gameObject.GetComponent<PlayerController>().slowStop();
		float time = 0.0f;
		while (time < Constants.PLAYER_ANIM_STOP_TIME + Constants.PLAYER_ANIM_ROTATE_TIME) {
			time += Time.deltaTime;
			yield return null;
		}
		// StartCoroutine(_player.onLevelComplete(timer));
		while (time < Constants.PLAYER_ANIM_CELEBRATION_TIME) {
			time += Time.deltaTime;
			yield return null;
		}
		_ui.levelComplete();
    }
}