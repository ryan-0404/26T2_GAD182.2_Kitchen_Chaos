using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PizzaInput : MonoBehaviour
{
    [Header("Required Reference")]
    [SerializeField] private PizzaController pizzaController;

    [Header("Input Delay")]
    [SerializeField] private float inputDelay = 2f;

    private bool inputEnabled;

    private void Start()
    {
        inputEnabled = false;

        StartCoroutine(EnableInputRoutine());
    }

    private void Update()
    {
        if (!inputEnabled)
        {
            return;
        }

        if (pizzaController == null)
        {
            return;
        }

        if (!pizzaController.CanShoot)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pizzaController.Shoot();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI())
            {
                return;
            }

            pizzaController.Shoot();
        }
    }

    private IEnumerator EnableInputRoutine()
    {
        yield return new WaitForSeconds(inputDelay);

        inputEnabled = true;
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
        {
            return false;
        }

        return EventSystem.current.IsPointerOverGameObject();
    }

    public void DisableInput()
    {
        inputEnabled = false;
    }
}