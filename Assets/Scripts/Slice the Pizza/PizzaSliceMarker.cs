using UnityEngine;

public class PizzaSliceMarker : MonoBehaviour
{
    [Header("Knife Rotation")]
    [SerializeField]
    private float rotationSpeed = 120f;

    private float currentAngle;
    private bool markerMoving;

    public float CurrentPercentage
    {
        get
        {
            return currentAngle / 360f * 100f;
        }
    }

    private void Start()
    {
        currentAngle = 0f;
        markerMoving = false;

        UpdateKnifeRotation();
    }

    private void Update()
    {
        if (!markerMoving)
        {
            return;
        }

        currentAngle +=
            rotationSpeed * Time.deltaTime;

        if (currentAngle >= 360f)
        {
            currentAngle -= 360f;
        }

        UpdateKnifeRotation();
    }

    private void UpdateKnifeRotation()
    {
        transform.localRotation =
            Quaternion.Euler(
                0f,
                0f,
                -currentAngle
            );
    }

    public void StartMoving()
    {
        markerMoving = true;
    }

    public void StopMoving()
    {
        markerMoving = false;
    }

    public void ResetMarker()
    {
        currentAngle = 0f;
        markerMoving = false;

        UpdateKnifeRotation();
    }
}