using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{

    [SerializeField] private Transform player; // Player'ýn Transform'u

    public bool turnTowardPlayer; //Oyuncuya doðru dön

    // Start is called before the first frame update
    void Start()
    {
        // Player'ýn Transform'unu bul
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(turnTowardPlayer)
        {
            // Player'a doðru dön
            LookAtPlayer();
        }
    }


    void LookAtPlayer()
    {
        // Player'a doðru yönel
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

}
