using System.Collections;
using UnityEngine;

public class NaveLife : MonoBehaviour
{
    public int health = 5;
    public float inmunityTimer = 0.5f;
    public float opacityChangeSpeed = 0.1f;

    private NaveController controller;
    private NaveEnergy energy;
    private BoxCollider2D boxC;

    private SpriteRenderer spr;
    private bool canReceiveDamage = true;

    private void Awake()
    {
        boxC = GetComponent<BoxCollider2D>();  
        energy = GetComponent<NaveEnergy>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        float h = Mejoras.instance.CheckUpgrade("health");
        health = (int)h;

        HeartsUI.instance.SetHearts(health);
        controller = GetComponent<NaveController>();
    }

    public void GetDamage(int damage)
    {
        if (!canReceiveDamage) return;

        if (health > 0)
        {
            health -= damage;
            SfxManager.instance.DamageEnemy();
            HeartsUI.instance.SetHearts(health);

            if (health <= 0)
            {
                boxC.enabled = false;
                energy.Death();
                controller.NotMove();
                EndManager.instance.EndGame();
            }
            else
            {
                StartCoroutine(InmunityTime());
            }

        }
    }

    IEnumerator InmunityTime()
    {
        canReceiveDamage = false;

        float timer = 0;

        while (timer < inmunityTimer)
        {
            spr.enabled = !spr.enabled;
            yield return new WaitForSeconds(opacityChangeSpeed);
            timer += opacityChangeSpeed;
        }

        spr.enabled = true;
        canReceiveDamage = true;
    }

    public void Death()
    {
        boxC.enabled = false;
        NaveDeath.instance.Die();
    }
}
