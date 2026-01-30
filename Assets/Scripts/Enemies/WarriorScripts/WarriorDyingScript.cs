using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDyingScript : MonoBehaviour
{
    [SerializeField] private int destroyTime = 2;

    [SerializeField] private WarriorAnimation warriorAnimation;
    [SerializeField] private WarriorMovement warriorMovement;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider hitbox;

    private void Awake()
    {
        if (GameSceneManager.Instance.GetCurrentLevel() >= 7)
        {
            destroyTime = 1;
        }
    }

    public void Dying()
    {
        StartCoroutine(DyingEnum());
    }

    IEnumerator DyingEnum()
    {
        warriorAnimation.EnemyDeathAnim();
 
        warriorMovement.moveTowardsPlayer = false;

        rb.freezeRotation = true;
        rb.useGravity = false;

        Destroy(hitbox);

        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
