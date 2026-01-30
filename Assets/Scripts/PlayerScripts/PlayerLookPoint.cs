using UnityEngine;

public class PlayerLookPoint: MonoBehaviour
{
    public float rotationSpeed = 10f; // Dönüþ hýzý

    void Update()
    {
        // Fare imlecinin dünya pozisyonunu al
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Yükseklik farkýný sýfýrla

            // Karakteri fare imlecine doðru döndür
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}