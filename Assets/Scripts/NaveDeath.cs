using UnityEngine;

public class NaveDeath : MonoBehaviour
{
    public static NaveDeath instance;

    public GameObject deathParticles;
    public GameObject fueguitos;

    private SpriteRenderer spr;

    void Awake()
    {
        instance = this;

        spr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        deathParticles.SetActive(false);
    }

    public void Die()
    {
        deathParticles.SetActive(true);
        fueguitos.SetActive(false);
        spr.enabled = false;
    }
}
