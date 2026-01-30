using UnityEngine;

public class WarriorAnimation : MonoBehaviour
{
    private Animator animator;
    private WarriorMovement warriorMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        warriorMovement = GetComponent<WarriorMovement>();

        animator.SetFloat("MoveSpeed", warriorMovement.moveSpeed / 2f);
    }

    public void EnemyDeathAnim()
    {
        animator.SetBool("Death", true);
    }
}