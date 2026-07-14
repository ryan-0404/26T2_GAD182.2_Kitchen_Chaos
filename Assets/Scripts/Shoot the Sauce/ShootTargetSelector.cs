using UnityEngine;

public class ShootTargetSelector : MonoBehaviour
{
    [Header("Five Target Lanes")]
    [SerializeField] private TargetLane[] targetLanes;

    [Header("Testing")]
    [Tooltip("-1 selects randomly. Use 0 to 4 to force a lane.")]
    [SerializeField] private int forcedLaneIndex = -1;

    private int selectedLaneIndex;

    public int SelectedLaneIndex => selectedLaneIndex;

    private void Start()
    {
        SelectLane();
    }

    private void SelectLane()
    {
        if (targetLanes == null || targetLanes.Length != 5)
        {
            Debug.LogError(
                "ShootTargetSelector requires exactly five target lanes.",
                this
            );

            return;
        }

        if (forcedLaneIndex >= 0 &&
            forcedLaneIndex < targetLanes.Length)
        {
            selectedLaneIndex = forcedLaneIndex;
        }
        else
        {
            selectedLaneIndex =
                Random.Range(0, targetLanes.Length);
        }

        for (int i = 0; i < targetLanes.Length; i++)
        {
            if (targetLanes[i] == null)
            {
                Debug.LogWarning(
                    $"Target lane {i} has not been assigned.",
                    this
                );

                continue;
            }

            if (i == selectedLaneIndex)
            {
                targetLanes[i].SetAsSuccessLane();
            }
            else
            {
                targetLanes[i].SetAsMissLane();
            }
        }
    }
}