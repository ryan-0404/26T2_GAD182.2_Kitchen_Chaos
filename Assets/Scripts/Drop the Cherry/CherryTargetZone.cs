using UnityEngine;

public class CherryTargetZone : MonoBehaviour
{
    public enum TargetZoneType
    {
        CupcakeLanding,
        Miss
    }

    [Header("Zone Settings")]
    [SerializeField] private TargetZoneType zoneType;

    [Header("Cupcake Score")]
    [SerializeField]
    [Range(0f, 1f)]
    private float scoreMultiplier = 1f;

    public bool IsMissZone
    {
        get
        {
            return zoneType == TargetZoneType.Miss;
        }
    }

    public float ScoreMultiplier
    {
        get
        {
            return scoreMultiplier;
        }
    }
}