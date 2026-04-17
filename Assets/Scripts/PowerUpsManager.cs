using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public static PowerUpsManager instance;

    public GameObject bulletPowerUp;
    public GameObject nukePowerUp;
    public GameObject inmunePowerUp;

    private void Awake()
    {
        instance = this;
    }
    
    public void SpawnRandom(Vector3 position)
    {
        GameObject powerUp;

        int num = Random.Range(0, 3);
        switch (num)
        {
            case 0: powerUp = bulletPowerUp; break;
            case 1: powerUp = nukePowerUp; break; 
            case 2: powerUp = inmunePowerUp; break;

            default: powerUp = bulletPowerUp; break;
        }
        
        Instantiate(bulletPowerUp, position, Quaternion.identity);
    }
}
