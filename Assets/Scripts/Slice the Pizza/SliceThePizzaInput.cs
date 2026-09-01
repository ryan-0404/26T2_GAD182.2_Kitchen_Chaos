using UnityEngine;
using UnityEngine.InputSystem;

public class SliceThePizzaInput : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField]
    private GameManagerSTP gameManagerSTP;

    private void Update()
    {
        if (gameManagerSTP == null)
        {
            return;
        }

        if (!gameManagerSTP.CanAcceptInput)
        {
            return;
        }

        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            gameManagerSTP.PlayerPressedSpace();
        }
    }
}