using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float playerSpeed = 5f;

    Animator m_Animator;
    CharacterController m_CharacterController;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    private bool isAttacking;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Gravity
        m_CharacterController.Move(new Vector3(0, -1, 0));

        // Get basic movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isRunning = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("isRunning", isRunning);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        // Check if player is attacking
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isAttacking = true;
            m_Animator.SetBool("isAttacking", isAttacking);
        }

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            isAttacking = false;
            m_Animator.SetBool("isAttacking", isAttacking);
        }
    }

    void OnAnimatorMove()
    {
        m_CharacterController.Move(m_Movement * Time.deltaTime * playerSpeed);
        transform.rotation = m_Rotation;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check for collision with a human
        if(hit.gameObject.tag == "Human")
        {
            Animator f_Animator = hit.gameObject.GetComponent<Animator>();

            // Check if collision happened while attacking
            if (isAttacking)
            {
                f_Animator.SetBool("isDying", true);
            }
        }
    }
}