using UnityEngine;

public class WizardAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void FireAnim()
    {
        animator.SetTrigger("Fire");
    }

    // FireSpeed parametresini ayarlayan fonksiyon
    public void SetFireSpeed(float speed)
    {
        animator.SetFloat("FireSpeed", speed);
    }

    public void MagicianDeathAnim()
    {
        animator.SetBool("Death", true);
    }
}