using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PizzaMovement : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameManagerSTS gameManagerSTS;

    [Header("Horizontal Movement")]
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float leftLimit = -7f;
    [SerializeField] private float rightLimit = 7f;

    [Header("Vertical Movement")]
    [SerializeField] private float upwardSpeed = 10f;

    [Header("Screen Boundary")]
    [SerializeField] private float topMissLimit = 6.5f;

    private Rigidbody2D pizzaRigidbody;

    private bool movingHorizontally;
    private bool hasBeenShot;
    private bool movingLeft;
    private bool movingRight;

    public bool HasBeenShot
    {
        get
        {
            return hasBeenShot;
        }
    }

    public bool CanMoveHorizontally
    {
        get
        {
            return movingHorizontally && !hasBeenShot;
        }
    }

    private void Awake()
    {
        pizzaRigidbody = GetComponent<Rigidbody2D>();

        pizzaRigidbody.bodyType = RigidbodyType2D.Kinematic;
        pizzaRigidbody.gravityScale = 0f;
    }

    private void Start()
    {
        ResetPizza();
    }

    private void Update()
    {
        if (gameManagerSTS == null)
        {
            return;
        }

        if (gameManagerSTS.GameCompleted)
        {
            return;
        }

        if (hasBeenShot &&
            transform.position.y >= topMissLimit)
        {
            gameManagerSTS.PizzaMissed();
        }
    }

    private void FixedUpdate()
    {
        if (gameManagerSTS == null)
        {
            return;
        }

        if (!gameManagerSTS.CanPlay)
        {
            return;
        }

        if (hasBeenShot)
        {
            MoveUpward();
        }
        else if (movingHorizontally)
        {
            MoveHorizontally();
        }
    }

    public void SetHorizontalInput(
        bool leftPressed,
        bool rightPressed
    )
    {
        movingLeft = leftPressed;
        movingRight = rightPressed;
    }

    private void MoveHorizontally()
    {
        float direction = 0f;

        if (movingLeft)
        {
            direction -= 1f;
        }

        if (movingRight)
        {
            direction += 1f;
        }

        Vector2 position =
            pizzaRigidbody.position;

        position.x +=
            direction *
            horizontalSpeed *
            Time.fixedDeltaTime;

        position.x = Mathf.Clamp(
            position.x,
            leftLimit,
            rightLimit
        );

        pizzaRigidbody.MovePosition(position);
    }

    public void Launch()
    {
        if (hasBeenShot)
        {
            return;
        }

        if (!movingHorizontally)
        {
            return;
        }

        hasBeenShot = true;
        movingHorizontally = false;

        movingLeft = false;
        movingRight = false;
    }

    private void MoveUpward()
    {
        Vector2 position =
            pizzaRigidbody.position;

        position.y +=
            upwardSpeed *
            Time.fixedDeltaTime;

        pizzaRigidbody.MovePosition(position);
    }

    public void ResetPizza()
    {
        if (spawnPoint == null)
        {
            Debug.LogError(
                "Pizza spawn point has not been assigned.",
                this
            );

            return;
        }

        pizzaRigidbody.position =
            spawnPoint.position;

        pizzaRigidbody.rotation = 0f;

        hasBeenShot = false;
        movingHorizontally = true;

        movingLeft = false;
        movingRight = false;
    }

    public void StopMovement()
    {
        hasBeenShot = false;
        movingHorizontally = false;

        movingLeft = false;
        movingRight = false;
    }
}