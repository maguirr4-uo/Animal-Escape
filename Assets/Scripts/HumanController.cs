using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class HumanController : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent pathfinder;
	Transform target;
	Animator m_Animator;
	public float visionRange;
	
	private bool isInRange;
	private bool isWalking;
    private bool isDying;
    // private int isAttacking;
	private bool _hasAnimator;

	void Start () {
		pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		m_Animator = GetComponent<Animator>();
		StartCoroutine (UpdatePath ());
	}

	void Update () {
		_hasAnimator = TryGetComponent(out m_Animator);

		float dist = Vector3.Distance(target.position, transform.position);
		if (dist < visionRange)
		{
			isInRange = true;
		}
		else
		{
			isInRange = false;
		}
	}

    private void FixedUpdate()
    {
		// Check if hit
		if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Die1"))
			isDying = true;

		if (isDying && m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
		{
			Destroy(gameObject);
		}
	}

    IEnumerator UpdatePath() {
		float refreshRate = 1f;

		while (target != null)
		{
			if (isInRange && !isDying)
			{
				Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
				if (_hasAnimator)
				{
					m_Animator.SetBool("isWalking", true);
				}
				pathfinder.SetDestination(targetPosition);
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}
}
