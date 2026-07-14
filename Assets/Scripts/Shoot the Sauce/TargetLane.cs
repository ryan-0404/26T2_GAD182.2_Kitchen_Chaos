using UnityEngine;

public class TargetLane : MonoBehaviour
{
    [Header("Lane Objects")]
    [SerializeField] private GameObject conveyor;
    [SerializeField] private GameObject missZone;

    public void SetAsSuccessLane()
    {
        if (missZone != null)
        {
            missZone.SetActive(false);
        }

        if (conveyor != null)
        {
            conveyor.SetActive(true);
        }
    }

    public void SetAsMissLane()
    {
        if (conveyor != null)
        {
            conveyor.SetActive(false);
        }

        if (missZone != null)
        {
            missZone.SetActive(true);
        }
    }
}