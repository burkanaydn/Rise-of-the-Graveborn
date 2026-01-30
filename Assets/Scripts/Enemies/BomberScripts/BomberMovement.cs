using UnityEngine;

public class BomberMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f;

    public bool setDead;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        if(!setDead)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }
}
