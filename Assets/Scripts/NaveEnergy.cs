using UnityEngine;

public class NaveEnergy : MonoBehaviour
{
    public float energyMax = 8;
    public float energyLossBySec = 1;

    public float energyAct;

    private NaveController controller;
    private NaveLife naveLife;
    private bool isDeath = false;

    private void Awake()
    {
        naveLife = GetComponent<NaveLife>();
    }

    void Start()
    {
        float energyM = Mejoras.instance.CheckUpgrade("energy");
        energyMax = energyM;


        energyAct = energyMax;    
        controller = GetComponent<NaveController>();
    }

    void Update()
    {
        if (!isDeath && energyAct > 0)
        {
            energyAct -= energyLossBySec * Time.deltaTime;
        
            if (energyAct < 0)
            {
                naveLife.Death();
                energyAct = 0;
                EnergyBarUI.instance.ShowBarToThePlayer();
                controller.NotMove();
                EndManager.instance.EndGame();
            }
        
            EnergyBarUI.instance.SetEnergyBar(energyAct / energyMax * 100);
        }
    }

    public void Death()
    {
        isDeath = true;
    }
}
