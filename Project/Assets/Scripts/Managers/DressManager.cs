using System;
using UnityEngine;

public class DressManager : MonoBehaviour {
    public bool isPreview;
    [SerializeField] private Transform _anchorHead;
    [SerializeField] private Transform _anchorTorso;
    [SerializeField] private Transform _anchorLegs;
    private GameObject _presentHead, _presentTorso, _presentLegs;
    [Header("********** Prefabs **********")]
    public GameObject[] headOptions;
    public GameObject[] torsoOptions;
    public GameObject[] legsOptions;

    public void changePart(PickupChoiceData.BodyPart part, int index) {
        Func<GameObject, Transform, GameObject> initPart = (GameObject partObj, Transform parent) => {
            GameObject obj = Instantiate(
                partObj,
                parent.position,
                Quaternion.identity, 
                parent
            );
            if (isPreview) {
                obj.layer = Constants.LAYER_PREVIEW;
            }
            return obj;
        };

        switch (part) {
        case PickupChoiceData.BodyPart.HEAD:
            if (_presentHead) {
                _presentTorso.SetActive(false);
            }
            _presentHead = initPart(headOptions[index], _anchorHead);
            break;
        case PickupChoiceData.BodyPart.TORSO:
            if (_presentTorso) {
                _presentTorso.SetActive(false);
            }
			_presentTorso = initPart(torsoOptions[index], _anchorTorso);
            break;
        case PickupChoiceData.BodyPart.LEGS:
            if (_presentLegs) {
                _presentLegs.SetActive(false);
            }
			_presentLegs = initPart(legsOptions[index], _anchorLegs);
            break;
        default:
            ErrorHandling.logError("Invalid body part type used");
            break;
        }
    }
}
