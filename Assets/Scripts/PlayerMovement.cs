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

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Gravity
        //if (m_CharacterController.isGrounded == false)
        //{
        //Add our gravity Vector
        m_CharacterController.Move(new Vector3(0, -1, 0));
        //}

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
        bool isAttacking;
        
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

        // TODO: Need to interact with human when attacking animation is playing, so that
        // human can be attacked.
    }

    void OnAnimatorMove()
    {
        m_CharacterController.Move(m_Movement * Time.deltaTime * playerSpeed);
        transform.rotation = m_Rotation;
    }
}