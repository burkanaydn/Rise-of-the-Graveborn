using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f; // Ateþ topu hýzý
    public float lifetime = 3f; // Ateþ topu ömrü (saniye)

    void Start()
    {
        // Ateþ topunu belirli bir süre sonra yok et
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Ateþ topunu ileri doðru hareket ettir
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Eðer çarpýþan objenin tag'i "Player" ise
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            SoundManager.Instance.PlayFireballOver();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}