using UnityEngine;

public class LightWizardAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayIdle()
    {
        animator.SetBool("IsCasting", false);
    }

    public void PlayCast()
    {
        animator.SetBool("IsCasting", true);
    }
    public void StopCast()
    {
        animator.SetBool("IsCasting", false);
    }

    public void PlayDeath()
    {
        animator.SetTrigger("IsDead");
    }

    public void ResetAll()
    {
        animator.SetBool("IsCasting", false);
    }
}
