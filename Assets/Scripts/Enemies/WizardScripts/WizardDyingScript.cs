using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardDyingScript : MonoBehaviour
{
    [SerializeField] private int destroyTime = 3;

    [SerializeField] private WizardAnimation wizardAnimation;
    [SerializeField] private WizardMovement wizardMovement;
    [SerializeField] private WizardFire wizardFire;

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
        wizardAnimation.MagicianDeathAnim();

        wizardFire.keepFire = false;

        wizardMovement.turnTowardPlayer = false;

        //Enemy'nin rotation dondur
        rb.freezeRotation = true;
        rb.useGravity = false;

        //Enemy'nin collider'ýný yok et
        Destroy(hitbox);

        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
