using UnityEngine;
using System.Collections;

public class DangerPanelUI : MonoBehaviour
{
    public static DangerPanelUI instance;

    public float speedScaler = 0.5f;
    public Vector3 maxScale = new Vector3(1f, 1f, 1f);
    public Vector3 minScale = new Vector3(0.5f, 0.5f, 1f);
    public float activeTime = 2f;

    public GameObject panel;
    public GameObject danger;

    private RectTransform dangerRt;
    
    private void Awake()
    {
        instance = this;

        dangerRt = danger.GetComponent<RectTransform>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HidePanel();
    }

    public void HidePanel()
    {
        panel.SetActive(false);
        danger.SetActive(false);
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        danger.SetActive(true);

        StartCoroutine(DangerScaler());
    }

    IEnumerator DangerScaler()
    {
        bool goUp = true;
        float timer = activeTime;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;

            Vector3 scaleToAchieve = (goUp ? maxScale : minScale);
            
            Vector3 newScale = Vector3.MoveTowards(dangerRt.localScale, scaleToAchieve, speedScaler * Time.deltaTime);
            dangerRt.localScale = newScale;

            if (dangerRt.localScale == scaleToAchieve) goUp = !goUp;
        }

        HidePanel();
    }
}
