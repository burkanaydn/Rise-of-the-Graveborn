using System;
using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private GolemAnimation golemAnimation;

    private bool canMove = true;
    private bool isDead = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (!canMove || isDead || player == null) return;

        // Yalnýzca yatay düzlemde yönlen
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            // Yönünü player’a çevir
            transform.rotation = Quaternion.LookRotation(direction.normalized);

            // Hareket
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;

            // Animasyon
            golemAnimation.PlayWalk();
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public void SetDead(bool value)
    {
        isDead = value;
        canMove = false;
    }
}
