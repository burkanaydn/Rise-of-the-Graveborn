using UnityEngine;

public class SmasherEffects : MonoBehaviour
{
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private GameObject impactEffectPrefab;

    public GameObject SpawnIndicator(Vector3 position)
    {
        if (indicatorPrefab == null) return null;

        GameObject indicator = Instantiate(indicatorPrefab, position, Quaternion.identity);
        return indicator;
    }

    public void SpawnImpactEffect(Vector3 position, float lifetime = 2f)
    {
        if (impactEffectPrefab == null) return;

        position.y = 0f; // zemine sabitle
        GameObject effect = Instantiate(impactEffectPrefab, position, Quaternion.identity);
        Destroy(effect, lifetime);
    }
}
