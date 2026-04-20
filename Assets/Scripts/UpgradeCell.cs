using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCell : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;

    [SerializeField] private UpgradeObject upgradeObject;

    public void ChangeBoxColor()
    {
        bool canTake = Mejoras.instance.CanTakeUpgrade(upgradeObject);
        if (upgradeObject.notImplemented)
        {
            image.color = Color.purple;
        }
        else if (canTake)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
            if (Mejoras.instance.GetCostNextUpgrade(upgradeObject) == -1)
            {
                Color newColor = Color.red;
                newColor.a = 0.8f;
                image.color = newColor;
            }
        }

        ActualizeText();
    }

    public void SetUpgradeObject(UpgradeObject upgObject)
    {
        upgradeObject = upgObject;

        ActualizeText();
    }

    public void OnPressCell()
    {
        bool upgraded = Mejoras.instance.TakeUpgrade(upgradeObject);

        if (!upgraded) SfxManager.instance.DamageEnemy();
        else SfxManager.instance.BallPoint();

        UpgradeCellsManager.instance.ActualizeAllCells();
    }

    private void ActualizeText()
    {
        int cost = Mejoras.instance.GetCostNextUpgrade(upgradeObject);
        text.text = upgradeObject.nombre + " - ";
        if (cost != -1) text.text += cost.ToString();
        else text.text += "MAX";
    }
}
