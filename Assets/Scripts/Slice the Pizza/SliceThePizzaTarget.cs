using TMPro;
using UnityEngine;

public class SliceThePizzaTarget : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text targetPercentageText;

    private int targetPercentage;

    public int TargetPercentage
    {
        get
        {
            return targetPercentage;
        }
    }

    private void Start()
    {
        ChooseTarget();

        if (targetPercentageText != null)
        {
            targetPercentageText.gameObject.SetActive(false);
        }
    }

    private void ChooseTarget()
    {
        int randomStep =
            Random.Range(1, 20);

        targetPercentage =
            randomStep * 5;

        if (targetPercentageText != null)
        {
            targetPercentageText.text =
                "Target: " +
                targetPercentage +
                "%";
        }
    }

    public void ShowTarget()
    {
        if (targetPercentageText != null)
        {
            targetPercentageText.gameObject.SetActive(true);
        }
    }
}