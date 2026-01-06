using UnityEngine;

public class SpiritMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private SpiritAnimation spiritAnimation;

    private SpiritState currentState = SpiritState.Idle;

    public void SetState(SpiritState newState)
    {
        currentState = newState;
        spiritAnimation.SetState(newState);
    }

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (currentState != SpiritState.Running || player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
