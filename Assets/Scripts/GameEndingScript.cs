using UnityEngine;

public class GameEndingScript : MonoBehaviour
{
    public static GameEndingScript Instance { get; private set; }

    [Header("Game Objects")]
    [SerializeField] private GameObject king;
    [SerializeField] private GameObject deathKing;
    [SerializeField] private Transform graveLid;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 162.995f; // derece/saniye

    private bool isRotating = false;
    private float currentRotation = 0f;

    private void Awake()
    {
        Instance = this;
    }

    public void GameEnding()
    {
        if (king != null) king.SetActive(false);
        if (deathKing != null) deathKing.SetActive(true);
        isRotating = true;
        currentRotation = 0f;
    }

    private void Update()
    {
        if (isRotating && graveLid != null)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            float remainingRotation = 197.005f - currentRotation;

            if (rotationStep > remainingRotation)
                rotationStep = remainingRotation;

            graveLid.Rotate(Vector3.right, rotationStep, Space.Self);
            currentRotation += rotationStep;

            if (currentRotation >= 197.005f)
            {
                isRotating = false;
            }
        }
    }
}
