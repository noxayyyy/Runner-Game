using System;
using UnityEngine;

public class PlayablePlane : MonoBehaviour {
    static public PlayablePlane instance;
    [NonSerialized] public Vector3 planeDimensions;

    void Awake() {
        if (instance && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
        planeDimensions = GetComponent<Renderer>().bounds.size;
    }
}
