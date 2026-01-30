using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    private GameOverManager gameOverManager => GameOverManager.Instance;
    void OnCollisionEnter(Collision collision)
    {
        // Eðer çarpýþan objenin tag'i "Enemy" ise
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameOverManager.GameisOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy Bullet") || other.CompareTag("WorldBorder"))
        {
            gameOverManager.GameisOver();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy Bullet"))
        {
            gameOverManager.GameisOver();
        }
    }
}
