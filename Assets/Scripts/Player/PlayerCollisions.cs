using UnityEngine;

public class PlayerCollisions : MonoBehaviour {
    static public Collider playerCollider;
    private GameManager _manager;
    private Rigidbody _body;
    private PlayerController _controller;
    [SerializeField] private float _maxSlopeAngle;
    
    void Awake() {
        playerCollider = gameObject.GetComponent<Collider>();
        _body = gameObject.GetComponent<Rigidbody>();
        _controller = gameObject.GetComponent<PlayerController>();
    }

    void Start() {
        _manager = GameManager.instance;
    }
    
    /* implement slope handling */
    void FixedUpdate() {
        float maxDist = 0.3f;
        if (!Physics.Raycast(gameObject.transform.position, transform.forward, out RaycastHit hit, maxDist)) {
            _controller.stopAxes(y:true);
            return;
        }
        float angle = Vector3.Angle(transform.up, hit.normal);
        if (!hit.transform.CompareTag(Constants.TAG_PLANE) || angle > _maxSlopeAngle) {
            return;
        }
        float yVel = _body.velocity.z * Mathf.Tan(angle * Mathf.PI / 180.0f);
        _controller.setVel(new Vector3(0.0f, yVel, 0.0f));
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag(Constants.TAG_FINISH_LINE)) {
            Player.instance.allowInput = false;
            StartCoroutine(_manager.endLevel());
        }
    }
}