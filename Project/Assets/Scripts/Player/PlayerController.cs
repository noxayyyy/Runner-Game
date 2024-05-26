using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody _body;
    private Animator _animator;
    private bool _moveDirection;
    private bool _isMovingX;
    [SerializeField] private float _stillRange;

    void Awake() {
        _body = gameObject.GetComponent<Rigidbody>();
        _animator = gameObject.GetComponent<Animator>();
    }


    public void autoMove() {
        _animator.SetBool(Constants.ANIMATOR_BOOL_STOPPED, false);
        DOTween.ToAxis(
            () => _body.velocity,
            z => _body.velocity = z,
            Constants.PLAYER_AUTOSPEED_Z,
            1.5f,
            AxisConstraint.Z
        )
        .SetUpdate(UpdateType.Fixed)
        .OnUpdate(() => {
            _animator.SetFloat(Constants.ANIMATOR_FLOAT_BLEND, _body.velocity.z / Constants.PLAYER_AUTOSPEED_Z);
        });
    }

    public void slowStop() {
        DOTween.ToAxis(
            () => _body.velocity,
            z => _body.velocity = z,
            0.0f,
            Constants.PLAYER_ANIM_STOP_TIME,
            AxisConstraint.Z
        )
        .SetUpdate(UpdateType.Fixed)
        .OnUpdate(() => {
            _animator.SetFloat(Constants.ANIMATOR_FLOAT_BLEND, _body.velocity.z / Constants.PLAYER_AUTOSPEED_Z);
        })
        .OnComplete(() => {
            _animator.SetBool(Constants.ANIMATOR_BOOL_STOPPED, true);
            gameObject.transform.DORotate(
                new Vector3(0.0f, 180.0f, 0.0f), 
                Constants.PLAYER_ANIM_ROTATE_TIME, 
                RotateMode.Fast
            )
            .OnComplete(() => {
                _animator.SetBool(Constants.ANIMATOR_BOOL_LEVELCOMPLETE, true);
            });
        });
    }

    // Sets velocity of player. Does not alter velocities for axes with magnitude 0
    public void setVel(Vector3 vel) {
        _body.velocity = new Vector3(
            vel.x == 0.0f ? _body.velocity.x : vel.x,
            vel.y == 0.0f ? _body.velocity.y : vel.y,
            vel.z == 0.0f ? _body.velocity.z : vel.z
        );
    }

    public void stopAxes(bool x = false, bool y = false, bool z = false) {
        _body.velocity = new Vector3(
            x ? 0 : _body.velocity.x,
            y ? 0 : _body.velocity.y,
            z ? 0 : _body.velocity.z
        );
    }

    public bool moveTowardsX(float inputPos) {
        bool direction = inputPos > transform.position.x;
        if (_isMovingX && direction == _moveDirection) {
            return true;
        } 
        
        float difference = Mathf.Abs(inputPos - transform.position.x);
        if (difference < _stillRange) {
            return false;
        }

        _moveDirection = direction;
        if (direction) {
            setVel(new Vector3(Constants.PLAYER_AUTOSPEED_X, 0.0f, 0.0f));
        } else {
            setVel(new Vector3(-Constants.PLAYER_AUTOSPEED_X, 0.0f, 0.0f));
        }
        return true;
    }

    public void animateFlair() {
        DOTween.To(
            () => _animator.GetFloat(Constants.ANIMATOR_FLOAT_BLEND),
            blend => _animator.SetFloat(Constants.ANIMATOR_FLOAT_BLEND, blend),
            1.0f,
            0.5f
        );
    }
}
