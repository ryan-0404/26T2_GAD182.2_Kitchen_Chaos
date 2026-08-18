using UnityEngine;

public enum PizzaTargetType
{
    Conveyor,
    MissZone
}

public class PizzaTargetZone : MonoBehaviour
{
    [Header("Target Type")]
    [SerializeField] private PizzaTargetType targetType;

    [Header("Conveyor Reference")]
    [Tooltip(
        "Required only when Target Type is Conveyor."
    )]
    [SerializeField] private ConveyorMover conveyorMover;

    public void ProcessPizzaCollision(
        PizzaController pizzaController
    )
    {
        if (pizzaController == null)
        {
            return;
        }

        if (targetType == PizzaTargetType.Conveyor)
        {
            ProcessConveyorCollision(pizzaController);
        }
        else
        {
            ProcessMissCollision(pizzaController);
        }
    }

    private void ProcessConveyorCollision(
        PizzaController pizzaController
    )
    {
        if (conveyorMover == null)
        {
            Debug.LogError(
                "A Conveyor target zone requires a ConveyorMover reference.",
                this
            );

            return;
        }

        pizzaController.LandOnConveyor(conveyorMover);
    }

    private void ProcessMissCollision(
        PizzaController pizzaController
    )
    {
        pizzaController.HitMissZone();
    }
}