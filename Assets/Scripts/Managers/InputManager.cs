using UnityEngine;

public class InputManager : InputDetector {
    private Touch _touch;

    float? handleTouches() {
        // properly implement touch handling
        if (Input.touchCount <= 0) {
            return null;
        }
        _touch = Input.GetTouch(0);

        if (_touch.phase == TouchPhase.Moved) {
            return _touch.deltaPosition.x;
        }
        return null;
    }
}
