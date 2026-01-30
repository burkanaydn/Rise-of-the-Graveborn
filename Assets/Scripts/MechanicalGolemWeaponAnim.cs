using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalGolemWeaponAnim : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.Rotate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }
}
