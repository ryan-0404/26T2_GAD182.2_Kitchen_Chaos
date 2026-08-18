using UnityEngine;

public class PizzaCollisionDetector : MonoBehaviour
{
    [Header("Required Reference")]
    [SerializeField] private PizzaController pizzaController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pizzaController == null)
        {
            return;
        }

        if (pizzaController.CurrentState != PizzaState.FlyingUpward)
        {
            return;
        }

        PizzaTargetZone targetZone =
            other.GetComponent<PizzaTargetZone>();

        if (targetZone == null)
        {
            targetZone =
                other.GetComponentInParent<PizzaTargetZone>();
        }

        if (targetZone == null)
        {
            return;
        }

        targetZone.ProcessPizzaCollision(pizzaController);
    }
}