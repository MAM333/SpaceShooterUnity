using UnityEngine;

public class LaserBoss : MonoBehaviour
{
    public int damage = 1;

    private bool attacking = false;
    private Animator anim;
    private BoxCollider2D boxC;
    private SpriteRenderer spr;

    private void Awake()
    {
        boxC = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        boxC.enabled = false;
        spr.enabled = false;
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void LaserUp()
    {
        boxC.enabled = true;
    }

    public void LaserDown()
    {
        boxC.enabled = false;
    }

    public void NoMoreLaser()
    {
        attacking = false;
        spr.enabled = false;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        spr.enabled = true;
        attacking = true;
    }
}
