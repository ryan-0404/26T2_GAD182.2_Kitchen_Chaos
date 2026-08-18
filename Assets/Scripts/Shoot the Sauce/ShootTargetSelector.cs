using UnityEngine;

public class ShootTargetSelector : MonoBehaviour
{
    [Header("Target Lanes")]
    [SerializeField] private TargetLane[] targetLanes;

    [Header("Testing")]
    [Tooltip(
        "-1 chooses randomly. Values 0 to 4 force a particular lane."
    )]
    [SerializeField] private int forcedLaneIndex = -1;

    private int selectedLaneIndex = -1;

    public int SelectedLaneIndex
    {
        get
        {
            return selectedLaneIndex;
        }
    }

    private void Start()
    {
        SelectTargetLane();
    }

    public void SelectTargetLane()
    {
        if (!ValidateLanes())
        {
            return;
        }

        selectedLaneIndex = ChooseLaneIndex();

        for (int i = 0; i < targetLanes.Length; i++)
        {
            bool isSelectedLane =
                i == selectedLaneIndex;

            targetLanes[i].ConfigureLane(
                isSelectedLane
            );
        }

        Debug.Log(
            "Shoot the Sauce success lane: " +
            selectedLaneIndex,
            this
        );
    }

    private int ChooseLaneIndex()
    {
        if (forcedLaneIndex >= 0 &&
            forcedLaneIndex < targetLanes.Length)
        {
            return forcedLaneIndex;
        }

        return Random.Range(
            0,
            targetLanes.Length
        );
    }

    private bool ValidateLanes()
    {
        if (targetLanes == null)
        {
            Debug.LogError(
                "Target Lanes has not been assigned.",
                this
            );

            return false;
        }

        if (targetLanes.Length != 5)
        {
            Debug.LogError(
                "ShootTargetSelector requires exactly five lanes.",
                this
            );

            return false;
        }

        for (int i = 0; i < targetLanes.Length; i++)
        {
            if (targetLanes[i] == null)
            {
                Debug.LogError(
                    "Target Lane element " +
                    i +
                    " has not been assigned.",
                    this
                );

                return false;
            }
        }

        return true;
    }
}