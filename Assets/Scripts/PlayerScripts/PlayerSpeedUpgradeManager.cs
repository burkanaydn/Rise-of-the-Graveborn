using UnityEngine;

public class PlayerSpeedUpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerLookPoint playerLookPoint;

    [SerializeField] private float moveSpeedUpgradeCount = 1f;
    [SerializeField] private float rotationSpeedUpgradeCount = 2.5f;

    public void SetPlayerSpeed(int speedLevel)
    {
        playerController.moveSpeed += moveSpeedUpgradeCount * speedLevel;
        playerLookPoint.rotationSpeed += rotationSpeedUpgradeCount * speedLevel;
    }
}
