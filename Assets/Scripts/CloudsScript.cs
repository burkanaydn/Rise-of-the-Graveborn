using UnityEngine;

public class CloudsScript : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // Bulutlarýn hareket hýzý

    private void Update()
    {
        transform.localPosition += Vector3.left * speed * Time.deltaTime;

        if (transform.localPosition.x <= -235f)
        {
            transform.localPosition = new Vector3(193f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
