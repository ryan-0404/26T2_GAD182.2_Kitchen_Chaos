using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float upwardSpeed = 1.5f;

    [Header("Vertical Positions")]
    [SerializeField] private float resetY = 2.5f;
    [SerializeField] private float topLimit = 6.5f;

    private float fixedXPosition;

    private void Awake()
    {
        fixedXPosition = transform.position.x;
    }

    private void OnEnable()
    {
        ResetPosition();
    }

    private void Update()
    {
        transform.position +=
            Vector3.up *
            upwardSpeed *
            Time.deltaTime;

        if (transform.position.y >= topLimit)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(
            fixedXPosition,
            resetY,
            transform.position.z
        );
    }
}