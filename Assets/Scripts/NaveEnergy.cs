using System.Collections;
using UnityEngine;

public class NaveEnergy : MonoBehaviour
{
    public float energyMax = 8;
    public float energyLossBySec = 1;

    public float energyAct;

    private NaveController controller;
    private NaveLife naveLife;
    private bool isDeath = false;
    private bool energyStop = false;

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
        if (energyStop) return;

        if (!isDeath && energyAct > 0)
        {
            energyAct -= energyLossBySec * Time.deltaTime;
        
            if (energyAct < 0)
            {
                naveLife.Death(true);
                energyAct = 0;
                EnergyBarUI.instance.ShowBarToThePlayer();
                controller.NotMove();
                EndManager.instance.EndGame();
            }
        
            EnergyBarUI.instance.SetEnergyBar(energyAct / energyMax * 100);
        }
    }

    public void RecoverEnergy(float percentage)
    {
        float sum = energyAct + percentage / 100 * energyMax;
        if (sum > energyMax) sum = energyMax;
        energyAct = sum;
    }

    public void StopLossingEnergy(float time)
    {
        StartCoroutine(DoNotLoseEnergy(time));
    }

    IEnumerator DoNotLoseEnergy(float time)
    {
        energyStop = true;

        float timer = time;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
        }

        energyStop = false;
    }

    public void Death(bool byEnergy)
    {
        isDeath = true;
        NaveDeath.instance.Die(byEnergy);
    }
}
