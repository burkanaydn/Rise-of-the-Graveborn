using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    public float rotationSpeed = 120f; // Saniyede 120 derece = 3 saniyede 360 derece

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}