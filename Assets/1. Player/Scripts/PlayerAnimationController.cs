using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public float walkSpeed = 1.5f;
    public float sprintSpeed = 5f;

    public Animator animator;
    private Vector2 _movementDirection = Vector2.zero;
    private bool _isSprinting = false;
    private bool _isMoving { get { return _movementDirection != Vector2.zero; } }

    void Awake()
    {
        SubscribeToInputSystem();

        if (animator == null)
            Debug.LogError("Animator component missing");
    }

    private void SubscribeToInputSystem()
    {
        PlayerInputController.input_OnMoveDelegate += OnMove;
        PlayerInputController.input_OnSprintDelegate += OnSprint;
    }

    private void Update()
    {
        AnimateCharacter();
    }

    public void OnMove(Vector2 movementDirection)
    {
        _movementDirection = movementDirection;
    }
    public void OnSprint(bool isSprinting)
    {
        _isSprinting = isSprinting;
    }

    private void AnimateCharacter()
    {
        float speed = _isSprinting ? sprintSpeed : _isMoving ? walkSpeed : 0f;

        animator.SetFloat("AnimationSpeed", speed* _movementDirection.magnitude);

    }

}
