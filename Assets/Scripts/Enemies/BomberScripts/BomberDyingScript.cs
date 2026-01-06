using System.Collections;
using UnityEngine;

public class BomberDyingScript : MonoBehaviour
{
    [SerializeField] private int destroyTime = 3;
    [SerializeField] private BomberAnimation bomberAnimation;
    [SerializeField] private BomberMovement bomberMovement;
    [SerializeField] private BomberExplosion bomberExplosion;
    [SerializeField] private GameObject bomb;
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
        bomberMovement.setDead = true;
        bomberExplosion.Explode();
        bomberAnimation.EnemyDeathAnim();

        Destroy(bomb);

        rb.freezeRotation = true;
        rb.useGravity = false;

        Destroy(hitbox);

        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
