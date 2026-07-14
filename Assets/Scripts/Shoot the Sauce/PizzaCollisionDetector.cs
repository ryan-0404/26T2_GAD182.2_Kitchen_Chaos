using UnityEngine;

public class PizzaCollisionDetector : MonoBehaviour
{
    [Header("Required Reference")]
    [SerializeField] private GameManagerSTS gameManagerSTS;

    private bool collisionHandled;

    private void OnEnable()
    {
        collisionHandled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionHandled || gameManagerSTS == null)
        {
            return;
        }

        if (other.CompareTag("ConveyorSuccess"))
        {
            collisionHandled = true;
            gameManagerSTS.PizzaLandedOnConveyor();
            return;
        }

        if (other.CompareTag("PizzaMissZone"))
        {
            collisionHandled = true;
            gameManagerSTS.PizzaMissed();
        }
    }
}