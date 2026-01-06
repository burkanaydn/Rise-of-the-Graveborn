using System.Collections;
using UnityEngine;

public class SpiritDyingScript : MonoBehaviour
{
    [SerializeField] private int destroyTime = 3;

    [Header("Components")]
    [SerializeField] private SpiritAnimation spiritAnimation;
    [SerializeField] private SpiritMovement spiritMovement;
    [SerializeField] private SpiritAttackController spiritAttackController;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider hitbox;


    private void Awake()
    {
        if (GameSceneManager.Instance.GetCurrentLevel() >= 7)
        {
            destroyTime = 1;
        }
    }

    public void Dying()
    {
        StartCoroutine(DyingEnum());
    }

    private IEnumerator DyingEnum()
    {
        // Durumlarý güncelle
        spiritMovement.SetState(SpiritState.Dead);
        spiritAttackController.SetDead(true);

        spiritAnimation.SetState(SpiritState.Dead);

        // Fiziksel iþlemler
        rb.freezeRotation = true;
        rb.useGravity = false;

        if (hitbox != null)
            Destroy(hitbox);

        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
