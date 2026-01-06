using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCube : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerLookPoint playerLookPoint;
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerLookPoint = FindObjectOfType<PlayerLookPoint>();

        playerController.moveSpeed += 4;
        playerLookPoint.rotationSpeed += 10;

        StartCoroutine(EffectDone());
    }

    IEnumerator EffectDone()
    {
        yield return new WaitForSeconds(10);

        playerController.moveSpeed -= 4;
        playerLookPoint.rotationSpeed -= 10;
        Destroy(gameObject);
    }
}
