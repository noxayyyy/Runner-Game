using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    static public Player instance;
    private PlayerController _controller;
    public bool allowInput = false;
    private bool allowMove = true;
    public bool isMovingGlobal = false;
    private bool _isMovingX = false;
    public bool hasInput = false;
    public bool slowStopFinish = false;
    public float Xconstraint;

    void Awake() {
        if (instance && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
        _controller = gameObject.GetComponent<PlayerController>();
    }

    bool updateMovement() {
        if (!allowMove && isMovingGlobal) {
            _controller.stopAxes(true, true, true);
            isMovingGlobal = false;
            _isMovingX = false;
            return false;
        } else if (!allowMove) {
            return false;
        }
        return true;
    }

    void Update() {
        hasInput = allowInput && InputDetector.detectInput();

        if (!updateMovement()) {
            return;
        }

        if (hasInput) {
            moveX();
        } else if (_isMovingX) {
            _controller.stopAxes(x:true);
            _isMovingX = false;
        }
    }

    public void startAutoMove() {
        isMovingGlobal = true;
        _controller.autoMove();
    }

    void moveX() {
        _isMovingX = true;
        Plane plane = new Plane(Vector3.forward, -Camera.main.transform.position.z - 5.0f);
        Vector3 hitPos = InputDetector.locateInputOnPlane(plane);
        if (!_controller.moveTowardsX(hitPos.x)) {
            _controller.stopAxes(x:true);
            _isMovingX = false;
        }
    }

    public IEnumerator onLevelComplete(float timer) {
        float time = 0.0f, blend = 0.0f;
        while (time < timer) {
            blend += 1.0f / ( time < timer / 2.0f ? timer / 2.0f : - timer / 2.0f) * Time.deltaTime;
            // _animator.SetFloat( Constants.ANIMATOR_FLOAT_BLEND, blend);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
