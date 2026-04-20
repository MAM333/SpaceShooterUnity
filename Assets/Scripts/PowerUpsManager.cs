using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public static PowerUpsManager instance;

    public GameObject bulletPowerUp;
    public GameObject energyPowerUp;
    public GameObject inmunePowerUp;

    private void Awake()
    {
        instance = this;
    }
    
    public void SpawnRandom(Vector3 position)
    {
        GameObject powerUp;

        int num = Random.Range(0, 4);
        switch (num)
        {
            case 0: powerUp = bulletPowerUp; break;
            case 1: powerUp = energyPowerUp; break; 
            case 2: powerUp = inmunePowerUp; break;

            default: powerUp = bulletPowerUp; break;
        }
        
        Instantiate(powerUp, position, Quaternion.identity);
    }
}
