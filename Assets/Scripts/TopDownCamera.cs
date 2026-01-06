using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target; // Karakter objesi
    public Vector3 offset = new Vector3(0, 10, 0); // Kameranýn karaktere göre pozisyonu
    public float zoomSpeed = 2f; // Zoom hýzýný belirler
    public float minZoom = 5f; // Minimum zoom mesafesi
    public float maxZoom = 15f; // Maksimum zoom mesafesi

    private float currentZoom;

    void Start()
    {
        currentZoom = offset.y; // Baþlangýçta offset yüksekliðini kullan

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scrollInput * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        offset.y = currentZoom;
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
