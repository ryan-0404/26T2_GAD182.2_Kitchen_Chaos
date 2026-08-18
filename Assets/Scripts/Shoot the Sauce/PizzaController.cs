using System.Collections;
using UnityEngine;

public enum PizzaState
{
    Waiting,
    MovingHorizontally,
    FlyingUpward,
    RidingConveyor,
    Respawning,
    Finished
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PizzaController : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private GameManagerSTS gameManagerSTS;
    [SerializeField] private Transform spawnPoint;

    [Header("Horizontal Movement")]
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private float leftLimit = -7f;
    [SerializeField] private float rightLimit = 7f;

    [Header("Launch Movement")]
    [SerializeField] private float upwardSpeed = 10f;

    [Header("Screen Boundaries")]
    [Tooltip("Crossing this height while flying counts as a miss.")]
    [SerializeField] private float flyingMissY = 6.5f;

    [Tooltip("Crossing this height while riding the conveyor completes the game.")]
    [SerializeField] private float successfulExitY = 6.5f;

    [Header("Respawning")]
    [SerializeField] private float respawnDelay = 0.15f;

    private Rigidbody2D pizzaRigidbody;
    private Collider2D pizzaCollider;
    private SpriteRenderer pizzaRenderer;

    private ConveyorMover currentConveyor;

    private PizzaState currentState;
    private int horizontalDirection = 1;

    public PizzaState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public bool CanShoot
    {
        get
        {
            return currentState == PizzaState.MovingHorizontally;
        }
    }

    private void Awake()
    {
        pizzaRigidbody = GetComponent<Rigidbody2D>();
        pizzaCollider = GetComponent<Collider2D>();
        pizzaRenderer = GetComponent<SpriteRenderer>();

        pizzaRigidbody.bodyType = RigidbodyType2D.Kinematic;
        pizzaRigidbody.gravityScale = 0f;

        currentState = PizzaState.Waiting;
    }

    private void Start()
    {
        ResetToSpawn();
    }

    private void Update()
    {
        if (currentState == PizzaState.FlyingUpward)
        {
            CheckForFlyingMiss();
        }
        else if (currentState == PizzaState.RidingConveyor)
        {
            CheckForSuccessfulExit();
        }
    }

    private void FixedUpdate()
    {
        if (gameManagerSTS != null &&
            gameManagerSTS.GameCompleted)
        {
            return;
        }

        switch (currentState)
        {
            case PizzaState.MovingHorizontally:
                MoveHorizontally();
                break;

            case PizzaState.FlyingUpward:
                MoveUpward();
                break;

            case PizzaState.RidingConveyor:
                RideConveyor();
                break;
        }
    }

    public void Shoot()
    {
        if (currentState != PizzaState.MovingHorizontally)
        {
            return;
        }

        if (gameManagerSTS != null &&
            gameManagerSTS.GameCompleted)
        {
            return;
        }

        currentState = PizzaState.FlyingUpward;
    }

    public void LandOnConveyor(ConveyorMover conveyor)
    {
        if (currentState != PizzaState.FlyingUpward)
        {
            return;
        }

        if (conveyor == null)
        {
            Debug.LogError(
                "PizzaController was given a null conveyor.",
                this
            );

            return;
        }

        currentConveyor = conveyor;
        currentState = PizzaState.RidingConveyor;

        /*
         * The pizza remains controlled by its own Rigidbody2D.
         * It copies the conveyor velocity instead of being parented.
         */
        pizzaRigidbody.linearVelocity = Vector2.zero;

        currentConveyor.SetCarryingPizza(true);
    }

    public void HitMissZone()
    {
        if (currentState != PizzaState.FlyingUpward)
        {
            return;
        }

        StartCoroutine(RespawnRoutine());
    }

    public void FinishGame()
    {
        currentState = PizzaState.Finished;

        pizzaRigidbody.linearVelocity = Vector2.zero;
    }

    private void MoveHorizontally()
    {
        Vector2 newPosition = pizzaRigidbody.position;

        newPosition.x +=
            horizontalDirection *
            horizontalSpeed *
            Time.fixedDeltaTime;

        if (newPosition.x >= rightLimit)
        {
            newPosition.x = rightLimit;
            horizontalDirection = -1;
        }
        else if (newPosition.x <= leftLimit)
        {
            newPosition.x = leftLimit;
            horizontalDirection = 1;
        }

        pizzaRigidbody.MovePosition(newPosition);
    }

    private void MoveUpward()
    {
        Vector2 newPosition = pizzaRigidbody.position;

        newPosition.y +=
            upwardSpeed *
            Time.fixedDeltaTime;

        pizzaRigidbody.MovePosition(newPosition);
    }

    private void RideConveyor()
    {
        if (currentConveyor == null)
        {
            StartCoroutine(RespawnRoutine());
            return;
        }

        Vector2 conveyorMovement =
            currentConveyor.CurrentVelocity *
            Time.fixedDeltaTime;

        Vector2 newPosition =
            pizzaRigidbody.position +
            conveyorMovement;

        pizzaRigidbody.MovePosition(newPosition);
    }

    private void CheckForFlyingMiss()
    {
        if (transform.position.y < flyingMissY)
        {
            return;
        }

        StartCoroutine(RespawnRoutine());
    }

    private void CheckForSuccessfulExit()
    {
        if (transform.position.y < successfulExitY)
        {
            return;
        }

        currentState = PizzaState.Finished;

        if (currentConveyor != null)
        {
            currentConveyor.SetCarryingPizza(false);
        }

        if (gameManagerSTS == null)
        {
            Debug.LogError(
                "GameManagerSTS has not been assigned to PizzaController.",
                this
            );

            return;
        }

        gameManagerSTS.CompleteMiniGame();
    }

    private IEnumerator RespawnRoutine()
    {
        if (currentState == PizzaState.Respawning ||
            currentState == PizzaState.Finished)
        {
            yield break;
        }

        currentState = PizzaState.Respawning;

        if (currentConveyor != null)
        {
            currentConveyor.SetCarryingPizza(false);
            currentConveyor = null;
        }

        SetPizzaVisible(false);

        yield return new WaitForSeconds(respawnDelay);

        if (gameManagerSTS != null &&
            gameManagerSTS.GameCompleted)
        {
            yield break;
        }

        ResetToSpawn();
        SetPizzaVisible(true);
    }

    private void ResetToSpawn()
    {
        if (spawnPoint == null)
        {
            Debug.LogError(
                "Spawn Point has not been assigned to PizzaController.",
                this
            );

            return;
        }

        currentConveyor = null;

        pizzaRigidbody.position = spawnPoint.position;
        pizzaRigidbody.rotation = 0f;
        pizzaRigidbody.linearVelocity = Vector2.zero;

        if (Random.Range(0, 2) == 0)
        {
            horizontalDirection = -1;
        }
        else
        {
            horizontalDirection = 1;
        }
        
        currentState = PizzaState.MovingHorizontally;
    }

    private void SetPizzaVisible(bool isVisible)
    {
        if (pizzaRenderer != null)
        {
            pizzaRenderer.enabled = isVisible;
        }

        if (pizzaCollider != null)
        {
            pizzaCollider.enabled = isVisible;
        }
    }
}