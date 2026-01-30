using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerItemEffects : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private bool isHelmet;
    [SerializeField] private Transform playerHead;
    [SerializeField] private bool isFireCircle;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerHead = GameObject.FindGameObjectWithTag("Head").transform;
    }

    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, 1f, playerTransform.position.z);

        if(isHelmet)
        {
            transform.position = new Vector3 (playerHead.position.x, playerHead.position.y + 0.118f, playerHead.position.z);
            transform.rotation = playerHead.rotation;
        }

        if (isFireCircle)

        {
            transform.position = new Vector3(playerTransform.position.x, 1f, playerTransform.position.z);
            transform.rotation = Quaternion.Euler(90f, 0, 0);
        }
    }
}
