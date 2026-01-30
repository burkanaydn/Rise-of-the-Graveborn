using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScripts : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // Gemilerin hareket hýzý

    private void Update()
    {
        transform.localPosition += Vector3.left * speed * Time.deltaTime;

        if (transform.localPosition.x <= -165f)
        {
            transform.localPosition = new Vector3(97f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
