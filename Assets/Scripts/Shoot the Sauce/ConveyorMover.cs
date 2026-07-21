using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float upwardSpeed = 1.5f;

    [Header("Loop Positions")]
    [SerializeField] private float resetY = 2.5f;
    [SerializeField] private float topLimitY = 6.5f;

    private float fixedX;
    private float fixedZ;

    private bool carryingPizza;

    public Vector2 CurrentVelocity
    {
        get
        {
            return Vector2.up * upwardSpeed;
        }
    }

    public bool CarryingPizza
    {
        get
        {
            return carryingPizza;
        }
    }

    private void Awake()
    {
        fixedX = transform.position.x;
        fixedZ = transform.position.z;
    }

    private void OnEnable()
    {
        carryingPizza = false;

        ResetPosition();
    }

    private void Update()
    {
        MoveUpward();

        if (!carryingPizza &&
            transform.position.y >= topLimitY)
        {
            ResetPosition();
        }
    }

    public void SetCarryingPizza(bool isCarryingPizza)
    {
        carryingPizza = isCarryingPizza;
    }

    private void MoveUpward()
    {
        Vector3 newPosition = transform.position;

        newPosition.x = fixedX;
        newPosition.y += upwardSpeed * Time.deltaTime;
        newPosition.z = fixedZ;

        transform.position = newPosition;
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(
            fixedX,
            resetY,
            fixedZ
        );
    }
}