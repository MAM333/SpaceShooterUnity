using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    public static PointsUI instance;

    public TextMeshProUGUI pointsText;

    private void Awake()
    {
        instance = this;
    }

    public void ChangePoints(int points)
    {
        pointsText.text = points.ToString();
    }
}