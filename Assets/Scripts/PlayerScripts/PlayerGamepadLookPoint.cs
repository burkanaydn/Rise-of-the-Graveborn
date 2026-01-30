using UnityEngine;

public class PlayerGamepadLookPoint : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        Vector2 rightStick = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));

        if (rightStick.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(rightStick.x, 0f, rightStick.y).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = transform.position.y;
                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
