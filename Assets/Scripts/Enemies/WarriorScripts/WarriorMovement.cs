using UnityEngine;

public class WarriorMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 3f; // Düþmanýn hareket hýzý
    [SerializeField] public float turnSpeed = 3f; // Düþmanýn hareket hýzý
    [SerializeField] private Transform player; // Player'ýn Transform'u

    public bool moveTowardsPlayer;
    void Start()
    {
        // Player'ýn Transform'unu bul
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //Baþta Player'a doðru hareket et
        moveTowardsPlayer = true;
    }

    void Update()
    {
        if (moveTowardsPlayer)
        {
            // Player'a doðru hareket et
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
            // Player'a doðru yönel
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Düþmaný player'a doðru döndür
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}