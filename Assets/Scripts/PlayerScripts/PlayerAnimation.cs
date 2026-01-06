using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Hareket yönünü al
        Vector3 direction = playerController.GetMovementDirection();

        // Hareket yönünü karakterin lokal eksenlerine göre hesapla
        Vector3 localDirection = transform.InverseTransformDirection(direction);

        // Animator parametrelerini güncelle
        animator.SetFloat("Horizontal", localDirection.x);
        animator.SetFloat("Vertical", localDirection.z);

        animator.SetFloat("MoveSpeed", playerController.moveSpeed / 5);
    }
}