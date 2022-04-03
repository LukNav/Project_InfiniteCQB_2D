using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimator : MonoBehaviour
{
    public Animator animator;
    public float walkSpeedMultiplier = 1.5f; //Blend tree named "Movement" has sprint animation, which y position we place here
    public float maximumWalkSpeed = 3f;
    private Vector3 _lastPos;

    void Update()
    {
        Vector3 movementDirection = (_lastPos - transform.position).normalized;
        float speed = movementDirection.magnitude* walkSpeedMultiplier;
        speed = Mathf.Clamp(speed, -maximumWalkSpeed, maximumWalkSpeed);
        animator.SetFloat("Speed", speed);
        _lastPos = transform.position;
    }
}
