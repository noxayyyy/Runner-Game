using UnityEngine;

public class Coins : MonoBehaviour {
    void OnTriggerEnter() {
        CoinsUI.instance.add(Constants.COINS_VALUE_EACH);
        gameObject.SetActive(false);
    }
}
