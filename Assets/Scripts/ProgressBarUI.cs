using UnityEngine;

public class ProgressBarUI : MonoBehaviour
{
    public static ProgressBarUI instance;

    public GameObject skull;
    public GameObject skullLeft, skullRight;

    private RectTransform skullRt;
    private Vector2 leftPos, rightPos;

    private void Awake()
    {
        instance = this;
        skullRt = skull.GetComponent<RectTransform>();

        RectTransform skullLeftRt = skullLeft.GetComponent<RectTransform>();
        RectTransform skullRightRt = skullRight.GetComponent<RectTransform>();
        leftPos = skullLeftRt.anchoredPosition;
        rightPos = skullRightRt.anchoredPosition;
        
        skullLeft.SetActive(false);
        skullRight.SetActive(false);
    }

    public void ActualizeBar(int actual, int final)
    {
        float percentage = (float)actual / (float)final;
        if (percentage > 1) percentage = 1; // Podria ser que se matasen a mas enemigos de los necesarios

        Vector2 newPosSkull = Vector2.Lerp(leftPos, rightPos, (float)actual / final);
        skullRt.anchoredPosition = newPosSkull;
    }
}
