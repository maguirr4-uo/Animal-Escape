using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerGirlMovement : MonoBehaviour
{
    Animator m_Animator;
    CharacterController m_CharacterController;

    // Temporary variable for PoC
    private bool dissapear = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gravity
        m_CharacterController.Move(new Vector3(0, -1, 0));

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Die1") && m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
