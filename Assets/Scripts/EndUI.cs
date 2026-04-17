using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public static EndUI instance;

    public Image backgroundEndImage;
    public float timeToFill = 0.5f;
    public bool ended = false;

    private float actTime = 0;

    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    void Start()
    {
        Color c = backgroundEndImage.color;
        c.a = 0;
        backgroundEndImage.color = c;
    }

    void Update()
    {
        if (actTime > 0)
        {
            actTime -= Time.deltaTime;
            Color c = backgroundEndImage.color;
            c.a = (timeToFill - actTime) / timeToFill;
            if (actTime < 0)
            {
                c.a = 1;
                ended = true;
            }
            backgroundEndImage.color = c;
        }
    }

    public bool Ended() { return ended; }

    public void End()
    {
        actTime = timeToFill;
    }
}
