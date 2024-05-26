using System;
using UnityEngine;

public class Choice : MonoBehaviour {
    [NonSerialized] public PickupChoiceData.BodyPart part;
    [NonSerialized] public int partIndex;
    [NonSerialized] public bool rightOrWrong;

    void OnTriggerEnter(Collider collider) {
        if (!collider.CompareTag(Constants.TAG_PLAYER)) {
            return;
        }
        GameManager.instance.pickupPart(part, partIndex, rightOrWrong);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
