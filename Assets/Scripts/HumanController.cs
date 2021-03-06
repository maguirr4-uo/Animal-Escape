using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class HumanController : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent pathfinder;
	Transform target;
	Animator m_Animator;
	AudioSource dyingSound;

	public float visionRange;
	public bool isDying;

	// Sound
	private bool sfxPlayed = false;

	private bool isInRange;
	private bool isWalking;
    // private int isAttacking;
	private bool _hasAnimator;

	public GameObject gameManager;
	LoadCharacter waveManager;

	void Start () {

		waveManager = gameManager.GetComponent<LoadCharacter>();

		pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		m_Animator = GetComponent<Animator>();
		dyingSound = GetComponent<AudioSource>();

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
        {
			isDying = true;

			if (!sfxPlayed)
			{
				dyingSound.time = 0.1f;
				dyingSound.Play();
				sfxPlayed = true;
			}
		}

		if (isDying && m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
		{
			Destroy(gameObject);
			waveManager.humansDown++;
		}
	}

    IEnumerator UpdatePath() {
		float refreshRate = 0.5f;

		while (target != null)
		{
			if (isInRange && !isDying)
			{
				Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
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
