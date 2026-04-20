using UnityEngine;

public enum PowerUpType
{
    None = 0,
    Bullets = 1,
    Inmune = 2,
    Energy = 3,
}

public class PowerUp : MonoBehaviour
{
    public float durationOfPowerup = 10;
    public float speedGoingDown = 3f;
    public float speedOfChanging = 3f;
    public float minOpacity = 0.7f;
    public PowerUpType powerUpType = PowerUpType.None;
    public Color colorNave;

    private float alpha = 0;
    private SpriteRenderer spr;
    private Rigidbody2D rb;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ColorChange();
        Movement();
    }

    public Color GetColor()
    {
        return colorNave;
    }

    public float GetDuration() 
    { 
        return durationOfPowerup; 
    }

    public PowerUpType GetPowerUpType()
    {
        return powerUpType;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("End"))
        {
            Destroy(gameObject);
        }
    }

    private void ColorChange()
    {
        Color c = spr.color;
        alpha += speedOfChanging * Time.deltaTime;
        float opacity = Mathf.Abs(Mathf.Sin(alpha));
        if (opacity < minOpacity) opacity = minOpacity;
        c.a = opacity;
        spr.color = c;
    }

    private void Movement()
    {
        rb.linearVelocityX = - speedGoingDown;
    }
}
