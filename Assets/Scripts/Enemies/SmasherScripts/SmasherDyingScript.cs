using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherDyingScript : MonoBehaviour
{
    [SerializeField] private int destroyTime = 3;

    [SerializeField] private SmasherAnimation smasherAnimation;
    [SerializeField] private SmasherMovement smasherMovement;
    [SerializeField] private SmasherSound smasherSound;


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
        smasherAnimation.PlayDeath();

        smasherMovement.SetDead(true);

        smasherSound.PlayExplosionSFX();

        rb.freezeRotation = true;
        rb.useGravity = false;

        Destroy(hitbox);

        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
