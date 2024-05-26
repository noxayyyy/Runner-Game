using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour {
	static public LoadManager instance;
    [SerializeField] private float _loadingDuration; // in seconds
	[SerializeField] private GameObject _splashPanel, _loadingPanel;
    private Slider _loadingBar;
	static private GameObject _panel;
    private float _proportion;
	private float _timer;
	public bool isLoading {
		get;
		private set;
	} = true;

	void Awake() {
		if (instance && instance != this) {
			Destroy(this);
			return;
		}
		instance = this;
		SceneManager.sceneLoaded += onSceneLoaded;
	}

	void onSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (scene.name == Constants.SCENE_SPLASH) {
			StartCoroutine(loadSplash());
		} else {
			StartCoroutine(loadNormal());
		}
	}

	void enablePanel(GameObject panel) {
		_panel = panel;
		_loadingBar = _panel.GetComponentInChildren<Slider>();
		_loadingBar.value = 0.0f;
		_timer = _loadingDuration;
		_proportion = _loadingBar.maxValue / _loadingDuration;
		_panel.SetActive(true);
	}

	IEnumerator loadSplash() {
		isLoading = true;
		enablePanel(_splashPanel);
		while (_timer > 0) {
			_timer -= Time.deltaTime;
			_loadingBar.value += _proportion * Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		_panel.SetActive(false);
		isLoading = false;
		SceneNavigator.goToMainMenu();
	}

	IEnumerator loadNormal() {
		isLoading = true;
		enablePanel(_loadingPanel);
		while (_timer > 0) {
			_timer -= Time.deltaTime;
			_loadingBar.value += _proportion * Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		_panel.SetActive(false);
		isLoading = false;
	}
}
