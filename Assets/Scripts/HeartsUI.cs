using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public static HeartsUI instance;

    [Header("Hearts")]
    public GameObject heart1;
    public GameObject heart2, heart3, heart4, heart5;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void SetHearts(int numHearts)
    {
        heart1.SetActive(numHearts >= 1);
        heart2.SetActive(numHearts >= 2);
        heart3.SetActive(numHearts >= 3);
        heart4.SetActive(numHearts >= 4);
        heart5.SetActive(numHearts >= 5);
    }
}
