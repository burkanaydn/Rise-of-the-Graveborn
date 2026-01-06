using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalGolemWeaponPartickle : MonoBehaviour
{
    [SerializeField] private GameObject weaponPartickle;
    void OnTriggerEnter(Collider other)
    {
        weaponPartickle.SetActive(false);

    }

}
