using UnityEngine;

public class BomberAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void EnemyDeathAnim()
    {
        animator.SetTrigger("Death");
    }
}
