using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;

    public AudioSource disparos;
    public AudioSource damageEnemy;
    public AudioSource ballPoint;
    public AudioSource laser;
    public AudioSource energyLoss;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        disparos.Play();
        damageEnemy.Play();
        ballPoint.Play();
        laser.Play();

        disparos.Stop();
        damageEnemy.Stop();
        ballPoint.Stop();
        laser.Stop();
    }

    public void Shooting(bool shooting)
    {
        if (shooting && !disparos.isPlaying) disparos.Play();
        else if (!shooting && disparos.isPlaying)disparos.Stop();
    }

    public void DamageEnemy()
    {
        damageEnemy.Play();
    }

    public void BallPoint()
    {
        ballPoint.Play();
    }

    public void LaserEnemy(AudioClip laserType)
    {
        laser.clip = laserType;
        laser.Play();
    }

    public void EnergyLoss()
    {
        energyLoss.Play();
    }
}
