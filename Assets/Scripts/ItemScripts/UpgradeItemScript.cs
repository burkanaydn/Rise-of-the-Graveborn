using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemScript : MonoBehaviour
{
    
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player")) // Eðer Player çarptýysa
        {
            ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>(); // Sahnedeki GameManager'ý bul

            int randomIndex = Random.Range(0, itemSpawner.playerEffects.Length); // Rastgele bir efekt seç
            Instantiate(itemSpawner.playerEffects[randomIndex], transform.position, Quaternion.identity); // Efekti oluþtur
            Destroy(gameObject);
        }
    }
}
