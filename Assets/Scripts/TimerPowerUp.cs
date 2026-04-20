using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class TimerPowerUp : MonoBehaviour
{
    public static TimerPowerUp instance;

    [Header("Sprites")]
    public Sprite timer1;
    public Sprite timer2, timer3, timer4, timer5, timer6, timer7, timer8;

    private SpriteRenderer spr;

    private void Awake()
    {
        instance = this;

        spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spr.enabled = false;
    }

    public void SetTimerPowerUp(float time)
    {
        StopAllCoroutines();
        StartCoroutine(TimerPowUp(time));
    }

    IEnumerator TimerPowUp(float time)
    {
        spr.enabled = true;

        float timer = time;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;

            Sprite selectSprite = null;

            float percentage = timer / time * 100;
            if (percentage > 87) selectSprite = timer1;
            else if (percentage > 75) selectSprite = timer2;
            else if (percentage > 62) selectSprite = timer3;
            else if (percentage > 50) selectSprite = timer4;
            else if (percentage > 37) selectSprite = timer5;
            else if (percentage > 25) selectSprite = timer6;
            else if (percentage > 12) selectSprite = timer7;
            else if (percentage > 0) selectSprite = timer8;

            spr.sprite = selectSprite;
        }

        spr.enabled = false;
    }
}
