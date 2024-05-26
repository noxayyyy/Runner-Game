using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private bool lockX, lockY, lockZ;
    [SerializeField] private float _diffX, _diffY, _diffZ;
    static private Vector3 _diffVec;
    static private Transform _camera;
    static private Vector3 _cameraDefault;
    static private Vector3 _tempPos;

    void Awake() {
        _diffVec = new Vector3(_diffX, _diffY, _diffZ);
    }

    void Start() {
        _camera = Camera.main.transform;
        _camera.position = transform.position + _diffVec;
        _cameraDefault = _camera.position;
    }
    
    void FixedUpdate() {
        // _camera.LookAt(transform);
    }
    
    void LateUpdate() {
        _tempPos = new Vector3(
            lockX ? _cameraDefault.x : transform.position.x + _diffX,
            lockY ? _cameraDefault.y : transform.position.y + _diffY,
            lockZ ? _cameraDefault.z : transform.position.z + _diffZ
        );
        _camera.position = _tempPos;
    }
}
