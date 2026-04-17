using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;

    public AudioSource disparos;
    public AudioSource damageEnemy;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        disparos.Play();
        disparos.Stop();
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
}
