using Unity.VisualScripting;
using UnityEngine;

public class InputDetector {
    static public bool detectInput() {
#if UNITY_EDITOR
        return Input.GetMouseButton((int)MouseButton.Left);
#endif
    }

    static public Vector3 locateInputOnPlane(Plane plane) {
        Vector3 hitPos = Vector3.zero;
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance)) {
            hitPos = ray.GetPoint(distance);
        }
        return hitPos;
    }
}
