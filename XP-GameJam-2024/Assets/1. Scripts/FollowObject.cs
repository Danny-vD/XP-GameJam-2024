using UnityEngine;
using UnityEngine.AI;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public float arrivalDistance = 1.0f;
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (target == null)
        {
            Debug.LogError("Target object is not assigned!");
        }
        else
        {
            StartCoroutine(UpdateDestination());
        }
    }

    private System.Collections.IEnumerator UpdateDestination()
    {
        while (true)
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
                animator.SetBool("Walking", true);

                if (Vector3.Distance(transform.position, target.position) <= arrivalDistance)
                {
                    animator.SetBool("Walking", false);
                }
            }
            else
            {
                animator.SetBool("Walking", false);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
