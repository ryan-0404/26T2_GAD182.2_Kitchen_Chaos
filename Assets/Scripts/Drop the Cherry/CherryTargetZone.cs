using UnityEngine;

public class CherryTargetZone : MonoBehaviour
{
    public enum TargetZoneType
    {
        CupcakeLanding,
        Miss
    }

    [Header("Zone Type")]
    [SerializeField] private TargetZoneType zoneType;

    public bool IsMissZone
    {
        get
        {
            return zoneType == TargetZoneType.Miss;
        }
    }
}