using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    public Vector3 leftPos, rightPos;

    private bool left = true;

    private void Awake()
    {
        instance = this;
    }

    public void ShakeCamera(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Shake(time));
    }

    IEnumerator Shake(float time)
    {
        float timer = time;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
            if (left) transform.localPosition = leftPos;
            else transform.localPosition = rightPos;
            left = !left;
        }

        transform.localPosition = new Vector3(0, 0, -10);
    }
}
