using UnityEngine;

public class EnemyCollison : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private ParticleSystem explodionEffect;
    [SerializeField] private GameObject pointLight;
    [SerializeField] private WizardDyingScript wizardDyingScript;
    [SerializeField] private WarriorDyingScript warriorDyingScript;
    [SerializeField] private BomberDyingScript bomberDyingScript;
    [SerializeField] private SpiritDyingScript spiritDyingScript;
    [SerializeField] private SmasherDyingScript smasherDyingScript;
    [SerializeField] private LightWizardDyingScript lightWizardDyingScript;
    [SerializeField] private GolemDyingScript golemDyingScript;
    [SerializeField] private bool enemyDie = false;

    private void Update()
    {
        if (LevelCompleteManager.Instance.levelIsOver && !enemyDie)
        {
            if (wizardDyingScript != null)
            {
                wizardDyingScript.Dying();
            }

            if (warriorDyingScript != null)
            {
                warriorDyingScript.Dying();
            }

            if (bomberDyingScript != null)
            {
                bomberDyingScript.Dying();
            }

            if (spiritDyingScript != null)
            {
                spiritDyingScript.Dying();
            }

            if (smasherDyingScript != null)
            {
                smasherDyingScript.Dying();
            }

            if (lightWizardDyingScript != null)
            {
                lightWizardDyingScript.Dying();
            }

            if (golemDyingScript != null)
            {
                golemDyingScript.Dying();
            }

            if (explodionEffect != null)
            {
                explodionEffect.Play();
            }


            bloodEffect.Play();

            pointLight.SetActive(false);

            enemyDie = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Shield"))
        {
            Destroy(other.gameObject);

            if(wizardDyingScript != null)
            {
                wizardDyingScript.Dying();
            }

            if(warriorDyingScript != null)
            {
                warriorDyingScript.Dying();
            }
            
            if(bomberDyingScript != null)
            {
                bomberDyingScript.Dying();
            }
            
            if(spiritDyingScript != null)
            {
                spiritDyingScript.Dying();
            }
            
            if(smasherDyingScript != null)
            {
                smasherDyingScript.Dying();
            }

            if (lightWizardDyingScript != null)
            {
                lightWizardDyingScript.Dying();
            }

            if (golemDyingScript != null)
            {
                golemDyingScript.Dying();
            }

            if (explodionEffect != null)
            {
                explodionEffect.Play();
            }


            bloodEffect.Play();

            pointLight.SetActive(false);

            ObjectPooler.Instance.SpawnFromPool("Coin", transform.position, Quaternion.identity);

            EnemySpawner.Instance.EnemyDefeated();
            ExpManager.Instance.AddExp(1);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (wizardDyingScript != null)
            {
                wizardDyingScript.Dying();
            }

            if (warriorDyingScript != null)
            {
                warriorDyingScript.Dying();
            }

            if (bomberDyingScript != null)
            {
                bomberDyingScript.Dying();
            }

            if (spiritDyingScript != null)
            {
                spiritDyingScript.Dying();
            }

            if (smasherDyingScript != null)
            {
                smasherDyingScript.Dying();
            }

            if (lightWizardDyingScript != null)
            {
                lightWizardDyingScript.Dying();
            }

            if (golemDyingScript != null)
            {
                golemDyingScript.Dying();
            }

            if (explodionEffect != null)
            {
                explodionEffect.Play();
            }


            bloodEffect.Play();

            pointLight.SetActive(false);

            ObjectPooler.Instance.SpawnFromPool("Coin", transform.position, Quaternion.identity);

            EnemySpawner.Instance.EnemyDefeated();
            ExpManager.Instance.AddExp(1);
        }
    }
}