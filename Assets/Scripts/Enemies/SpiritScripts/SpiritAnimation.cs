using UnityEngine;

public class SpiritAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetState(SpiritState state)
    {
        ResetAll();

        switch (state)
        {
            case SpiritState.Running:
                animator.SetBool("isRunning", true);
                break;

            case SpiritState.Charging:
                animator.SetTrigger("Charge");
                break;

            case SpiritState.Dashing:
                animator.SetTrigger("Dash");
                break;

            case SpiritState.Dead:
                animator.SetBool("Death", true);
                break;
        }
    }

    public void ResetAll()
    {
        animator.ResetTrigger("Charge");
        animator.ResetTrigger("Dash");
        animator.SetBool("isRunning", false);
        animator.SetBool("Death", false);
    }
}
