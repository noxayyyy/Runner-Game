using System;
using DG.Tweening;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    static public LevelManager instance;
	[SerializeField] public Transform playerSpawn;
    [SerializeField] private GameObject choiceFramePrefab, _rowPrefab;
    [SerializeField] public PickupChoiceData[] dataArr;
    [NonSerialized] public int points, totalPoints;
    [NonSerialized] public GameObject[] choiceFrames;
    public float winThreshold;
    private PlayablePlane _plane;
    private GameObject[] _rows;
    private DressManager _dress;

    void Awake() {
        if (instance && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
        points = 0;
        totalPoints = dataArr.Length;
        _rows = new GameObject[dataArr.Length];
        choiceFrames = new GameObject[0];
    }

    void initFrames(PickupChoiceData data, GameObject row) {
        // implement dividing frames across the plane
        // Vector3 posProportion = new Vector3(_plane.planeDimensions.x / (data.partIndexes.Length + 2), 0, 0);
        GameObject[] frames = new GameObject[data.partIndexes.Length];
        int multiplier = -(data.partIndexes.Length % 2 - 1); // if mod is 1, mult = 0 and vice versa
        int count = (multiplier == 0) ? -1 : 0;
        float c = 1.0f;
        for (int i = 0; i < data.partIndexes.Length; i++) {
            frames[i] = Instantiate(
                choiceFramePrefab,
                row.transform.position,
                Quaternion.identity,
                row.transform
            );
            frames[i].transform.position += new Vector3(
                frames[i].GetComponent<BoxCollider>().bounds.size.x / 2.0f,
                0.0f,
                0.0f
            ) * multiplier * c;
            c = -c;
            Choice frame = frames[i].GetComponent<Choice>();
            frame.part = data.part;
            frame.partIndex = data.partIndexes[i];
            frame.rightOrWrong = data.rightOrWrong[i];
            if (count % 2 != 0) {
                multiplier++;
            }
            count++;
        }
        choiceFrames = Helpers.extendArray(choiceFrames, frames);
    }

    void initPrefabs(PickupChoiceData data, GameObject[] frameRow) {
        Action<GameObject[]> initPickup = (GameObject[] objs) => {
            for (int i = 0; i < data.partIndexes.Length; i++) {
                GameObject obj = Instantiate(
                    objs[data.partIndexes[i]],
                    frameRow[i].transform.position,
                    Quaternion.identity, 
                    frameRow[i].transform
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
        };

        switch (data.part) {
        case PickupChoiceData.BodyPart.HEAD:
            initPickup(_dress.headOptions);
            break;
        case PickupChoiceData.BodyPart.TORSO:
            initPickup(_dress.torsoOptions);
            break;
        case PickupChoiceData.BodyPart.LEGS:
            initPickup(_dress.legsOptions);
            break;
        default:
            ErrorHandling.logError("Invalid body part type used");
            break;
        }
    }

    // Start is called before the first frame update
    void Start() {
        _plane = PlayablePlane.instance;
		_dress = Player.instance.GetComponent<DressManager>();
        Vector3 posProportion = new Vector3(0, 0, _plane.planeDimensions.z / (dataArr.Length + 1));
        for (int i = 0; i < dataArr.Length; i++) {
            _rows[i] = Instantiate(
                _rowPrefab,
                posProportion * (i + 1) + new Vector3(0.0f, 1.5f, 0.0f),
                Quaternion.identity,
                transform
            );
        }

        Func<int, GameObject[]> extractFrames = (int index) => {
            int startIndex = 0;
            for (int i = 0; i < index; i++) {
                startIndex += dataArr[i].partIndexes.Length;
            }
            int endIndex = startIndex + dataArr[index].partIndexes.Length;
            GameObject[] arr = new GameObject[endIndex - startIndex];
            for (int i = startIndex, j = 0; i < endIndex; i++, j++) {
                arr[j] = choiceFrames[i]; 
            } 
            return arr;
        };


        for (int i = 0; i < dataArr.Length; i++) {
            initFrames(dataArr[i], _rows[i]);
        }
        for (int i = 0; i < dataArr.Length; i++) {
            GameObject[] frameRow = extractFrames(i);
            initPrefabs(dataArr[i], frameRow);
        }
    }
}
