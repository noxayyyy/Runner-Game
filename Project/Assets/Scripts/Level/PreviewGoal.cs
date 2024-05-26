using System;
using UnityEngine;

public class PreviewGoal : MonoBehaviour {
    static public PreviewGoal instance;
    [SerializeField] private GameObject _playerPreview;
    private DressManager _previewDress;
    private LevelManager _level;
    private Tuple<PickupChoiceData.BodyPart, int>[] _dressChoices;

    void Awake() {
        if (instance && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
        _previewDress = _playerPreview.GetComponent<DressManager>();
    }

    void addPreviewPart( PickupChoiceData data, int index) {
        for (int i = 0; i < data.partIndexes.Length; i++) {
            if (!data.rightOrWrong[i]) {
                continue;
            }
            _dressChoices[index] = new Tuple<PickupChoiceData.BodyPart, int>( 
                data.part, 
                data.partIndexes[i]
            );
            return;
        }
    }

    // Start is called before the first frame update
    void Start() {
        // init preview
        _level = LevelManager.instance;
        _dressChoices = new Tuple<PickupChoiceData.BodyPart, int>[_level.dataArr.Length];
        int index = 0;

        // start adding parts
        foreach (var data in _level.dataArr) {
            addPreviewPart(data, index);
            index++;
        }
        foreach (var choice in _dressChoices) {
            _previewDress.changePart(choice.Item1, choice.Item2);
        }
    }
}
