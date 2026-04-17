using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour
{
    public static EnergyBarUI instance;

    [Header("Images")]
    public Image energyBarImage;
    public Sprite energyBar100, energyBar87, energyBar75, energyBar62, energyBar50, energyBar37, energyBar25, energyBar12, energyBar0;

    [Header("ShowBar")]
    public float scaleSpeed;    
    public Vector3 maxScale, minScale;

    private bool showingBar = false;
    private bool upScale = true;
    private RectTransform energyBarTransform;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        SetEnergyBar(100);
        energyBarTransform = energyBarImage.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (showingBar)
        {
            ShowBarBehaviour();
        }
    }

    public void ShowBarToThePlayer()
    {
        showingBar = true;
        upScale = true;
    }

    public void SetEnergyBar(float energy)
    {
        if (energy > 87) ActivateBar(100);
        else if (energy > 75) ActivateBar(87);
        else if (energy > 62) ActivateBar(75);
        else if (energy > 50) ActivateBar(62);
        else if (energy > 37) ActivateBar(50);
        else if (energy > 25) ActivateBar(37);
        else if (energy > 12) ActivateBar(25);
        else if (energy > 0) ActivateBar(12);
        else ActivateBar(0);
    }

    private void ShowBarBehaviour()
    {
        if (upScale) 
        { 
            Vector3 res = Vector3.MoveTowards(energyBarTransform.localScale, maxScale, scaleSpeed * Time.deltaTime);
            energyBarTransform.localScale = res;
            if (energyBarTransform.localScale == maxScale) upScale = false;
        }
        else 
        {
            Vector3 res = Vector3.MoveTowards(energyBarTransform.localScale, minScale, scaleSpeed * Time.deltaTime);
            energyBarTransform.localScale = res;
            if (energyBarTransform.localScale == minScale) upScale = true;
        }
    }

    private void ActivateBar(int bar)
    {
        Sprite energyBar = null;
        
        switch (bar)
        {
            case 100: energyBar = energyBar100; break;
            case 87: energyBar = energyBar87; break;
            case 75: energyBar = energyBar75; break;
            case 62: energyBar = energyBar62; break;
            case 50: energyBar = energyBar50; break;
            case 37: energyBar = energyBar37; break;
            case 25: energyBar = energyBar25; break;
            case 12: energyBar = energyBar12; break;
            case 0: energyBar = energyBar0; break;
        }

        energyBarImage.sprite = energyBar;
    }
}
