using UnityEngine;
using System.Collections;

public class Numbers : MonoBehaviour
{
    public static Numbers instance;
    [Header("Numbers")]
    public Sprite num1;
    public Sprite num3, num4, num10, num20, num30, num100, num250, num400, num9999;

    [Header("ShowNumber")]
    public Vector3 initPos;
    public Vector3 finalPos;
    public float timeShowed = 1f;
    public float timeToSpawn = 0.5f;
    public float timeToGo = 0.2f;

    private SpriteRenderer spr;

    void Awake()
    {
        instance = this;

        spr = GetComponent<SpriteRenderer>();
    }

    public void ShowNumber(float number)
    {
        StopAllCoroutines();
        switch (number)
        {
            case 1f: StartCoroutine(ShowNum(num1)); break;
            case 3f: StartCoroutine(ShowNum(num3)); break;
            case 10f: StartCoroutine(ShowNum(num10)); break;
            case 20f: StartCoroutine(ShowNum(num20)); break;
            case 30f: StartCoroutine(ShowNum(num30)); break;
            case 100f: StartCoroutine(ShowNum(num100)); break;
            case 250f: StartCoroutine(ShowNum(num250)); break;
            case 400f: StartCoroutine(ShowNum(num400)); break;
            case 9999f: StartCoroutine(ShowNum(num9999)); break;
        }
    }

    IEnumerator ShowNum(Sprite num)
    {
        transform.localPosition = initPos;

        spr.sprite = num;

        float timer = timeToSpawn;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;

            Vector3 res = Vector3.Lerp(transform.localPosition, finalPos, timeToSpawn - timer);
            transform.localPosition = res;
        }

        transform.localPosition = finalPos;

        timer = timeShowed;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
        }

        timer = timeToSpawn;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;

            Vector3 res = Vector3.Lerp(transform.localPosition, initPos, timeToGo - timer);
            transform.localPosition = res;
        }

        timer = timeToGo;

        spr.sprite = null;
    }
}
