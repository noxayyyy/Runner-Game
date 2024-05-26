using UnityEngine;
using DG.Tweening;

public class CoinsManager : MonoBehaviour {
    static public CoinsManager instance;
	static public int count = 0;
	[SerializeField] private GameObject _coinPrefab;

	void Awake() {
		if (instance && instance != this) {
			Destroy(this);
			return;
		}
		instance = this;
		count = loadCoins();
	}

	static public int loadCoins() {
		return DataPersistenceManager.loadInt(Constants.PREF_COINS);
	}

	static public void saveCoins(int coins) {
		DataPersistenceManager.save(Constants.PREF_COINS, coins);
	}

	void Start() {
		float length = PlayablePlane.instance.planeDimensions.z;
		for (int i = 0; i < length; i++) {
			GameObject obj = Instantiate(
				_coinPrefab,
				new Vector3(0.0f, transform.position.y, i),
				Quaternion.identity,
				transform
			);
			obj.transform.DORotate(
				new Vector3(0.0f, 360.0f, 0.0f),
				3.0f,
				RotateMode.Fast
			)
			.SetLoops(-1)
			.SetEase(Ease.Linear)
			.SetRelative(true);
		}
	}

	void onLevelComplete() {
		saveCoins(count);
	}
}
