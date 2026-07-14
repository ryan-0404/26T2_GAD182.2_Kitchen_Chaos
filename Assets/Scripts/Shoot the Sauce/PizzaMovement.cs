using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PizzaMovement : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameManagerSTS gameManagerSTS;

    [Header("Horizontal Movement")]
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private float leftLimit = -7f;
    [SerializeField] private float rightLimit = 7f;

    [Header("Upward Movement")]
    [SerializeField] private float verticalShootSpeed = 10f;

    [Header("Miss Boundary")]
    [Tooltip("The pizza counts as missed when it passes this Y position.")]
    [SerializeField] private float topMissLimit = 6.5f;

    private Rigidbody2D pizzaRigidbody;

    private int horizontalDirection = 1;

    private bool movingSideways;
    private bool hasBeenShot;
    private bool missReported;

    public bool HasBeenShot => hasBeenShot;

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
        if (gameManagerSTS == null || !gameManagerSTS.CanPlay)
        {
            return;
        }

        if (hasBeenShot &&
            !missReported &&
            transform.position.y >= topMissLimit)
        {
            missReported = true;
            gameManagerSTS.PizzaMissed();
        }
    }

    private void FixedUpdate()
    {
        if (gameManagerSTS == null || !gameManagerSTS.CanPlay)
        {
            return;
        }

        if (movingSideways)
        {
            MoveSideways();
        }
        else if (hasBeenShot)
        {
            MoveUpward();
        }
    }

    public void Launch()
    {
        if (!movingSideways ||
            hasBeenShot ||
            gameManagerSTS == null ||
            !gameManagerSTS.CanPlay)
        {
            return;
        }

        movingSideways = false;
        hasBeenShot = true;
        missReported = false;
    }

    private void MoveSideways()
    {
        Vector2 position = pizzaRigidbody.position;

        position.x +=
            horizontalDirection *
            horizontalSpeed *
            Time.fixedDeltaTime;

        if (position.x >= rightLimit)
        {
            position.x = rightLimit;
            horizontalDirection = -1;
        }
        else if (position.x <= leftLimit)
        {
            position.x = leftLimit;
            horizontalDirection = 1;
        }

        pizzaRigidbody.MovePosition(position);
    }

    private void MoveUpward()
    {
        Vector2 position =
            pizzaRigidbody.position +
            Vector2.up *
            verticalShootSpeed *
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

        pizzaRigidbody.position = spawnPoint.position;
        pizzaRigidbody.rotation = 0f;

        horizontalDirection =
            Random.value < 0.5f ? -1 : 1;

        movingSideways = true;
        hasBeenShot = false;
        missReported = false;
    }

    public void StopMovement()
    {
        movingSideways = false;
        hasBeenShot = false;
    }

    public void HidePizza()
    {
        gameObject.SetActive(false);
    }

    public void ShowPizza()
    {
        gameObject.SetActive(true);
    }
}