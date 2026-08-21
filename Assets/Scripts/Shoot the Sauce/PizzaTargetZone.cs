using UnityEngine;

public class PizzaTargetZone : MonoBehaviour
{
    public enum TargetZoneType
    {
        Conveyor,
        MissZone
    }

    [Header("Target Type")]
    [SerializeField] private TargetZoneType targetType;

    public bool IsMissZone
    {
        get
        {
            return targetType == TargetZoneType.MissZone;
        }
    }
}