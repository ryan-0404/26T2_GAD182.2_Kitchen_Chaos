using UnityEngine;

public class TargetLane : MonoBehaviour
{
    [Header("Lane Objects")]
    [SerializeField] private GameObject conveyorObject;
    [SerializeField] private GameObject missZoneObject;
    [SerializeField] private GameObject conveyorBackgroundObject;

    private bool successLane;

    public bool IsSuccessLane
    {
        get
        {
            return successLane;
        }
    }

    public void ConfigureLane(bool shouldBeSuccessLane)
    {
        successLane = shouldBeSuccessLane;

        if (conveyorObject != null)
        {
            conveyorObject.SetActive(shouldBeSuccessLane);
        }

        if (missZoneObject != null)
        {
            missZoneObject.SetActive(!shouldBeSuccessLane);
        }

        if (conveyorBackgroundObject != null)
        {
            conveyorBackgroundObject.SetActive(shouldBeSuccessLane);
        }
    }
}