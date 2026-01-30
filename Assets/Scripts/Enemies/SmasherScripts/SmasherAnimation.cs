using UnityEngine;

public class SmasherAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayIdle()
    {
        animator.SetBool("isIdle", true);
    }

    public void PlayJump()
    {
        animator.SetBool("isIdle", false);
        animator.SetTrigger("Jump");
    }

    public void PlayDeath()
    {
        animator.SetTrigger("Death");
    }

    public void ResetAll()
    {
        animator.ResetTrigger("Jump");
        animator.SetBool("isIdle", false);
        animator.SetBool("Death", false);
    }
}
