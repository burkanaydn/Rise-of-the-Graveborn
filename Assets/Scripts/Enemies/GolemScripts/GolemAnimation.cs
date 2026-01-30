using UnityEngine;

public class GolemAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayWalk()
    {
        animator.SetBool("isWalking", true);
    }

    public void StopWalk()
    {
        animator.SetBool("isWalking", false);
    }

    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayDeath()
    {
        animator.SetTrigger("Death");
    }
}
