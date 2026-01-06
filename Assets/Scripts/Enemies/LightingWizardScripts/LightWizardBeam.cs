using UnityEngine;

public class LightWizardBeam : MonoBehaviour
{
    [Header("Beam Objects")]
    [SerializeField] private Transform beamTransform;
    [SerializeField] private ParticleSystem beamEffect;

    private bool isBeamActive = false;
    void Update()
    {
        if (!isBeamActive || beamTransform == null) return;

        transform.position = beamTransform.position;
        transform.rotation = beamTransform.rotation;
    }

    public void EnableBeam()
    {
        if (beamEffect != null)
        {
            beamEffect.Play();
            isBeamActive = true;
        }
    }

    public void DisableBeam()
    {
        if (beamEffect != null)
        {
            beamEffect.Stop();
            isBeamActive = false;
        }  
    }
}
