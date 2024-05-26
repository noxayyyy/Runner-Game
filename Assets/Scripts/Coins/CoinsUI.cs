using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour {
	static public CoinsUI instance;
	static private Text _text;
	static private string _label = "Coins: ";

	void Awake() {
		if (instance && instance != this) {
			Destroy(this);
			return;
		}
		instance = this;
		CoinsManager.count = CoinsManager.loadCoins();
		_text = gameObject.GetComponentInChildren<Text>();
		updateText();
	}

	public void updateText() {
		_text.text = _label + CoinsManager.count;
	}

	public void add(int i) {
		CoinsManager.count += i;
	}

	public void subtract(int i) {
		CoinsManager.count -= i;
	}

	public void multiply(int i) {
		CoinsManager.count *= i;
	}

	public void divide(int i) {
		CoinsManager.count /= i;
	}

	void OnDisable() {
		CoinsManager.saveCoins(CoinsManager.count);
	}
}