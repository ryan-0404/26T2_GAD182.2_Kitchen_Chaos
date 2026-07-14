using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootInput : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private PizzaMovement pizzaMovement;
    [SerializeField] private GameManagerSTS gameManagerSTS;

    [Header("Input Delay")]
    [Tooltip("Time after the scene loads before shooting is allowed.")]
    [SerializeField] private float inputDelay = 2f;

    private bool inputEnabled;

    private void Start()
    {
        inputEnabled = false;
        StartCoroutine(EnableInputAfterDelay());
    }

    private IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(inputDelay);

        inputEnabled = true;
    }

    private void Update()
    {
        if (!inputEnabled ||
            pizzaMovement == null ||
            gameManagerSTS == null ||
            !gameManagerSTS.CanPlay)
        {
            return;
        }

        bool spacePressed =
            Input.GetKeyDown(KeyCode.Space);

        bool mousePressed =
            Input.GetMouseButtonDown(0);

        if (!spacePressed && !mousePressed)
        {
            return;
        }

        // Prevents UI clicks, such as pressing Pause,
        // from also launching the pizza.
        if (mousePressed &&
            EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        pizzaMovement.Launch();
    }
}