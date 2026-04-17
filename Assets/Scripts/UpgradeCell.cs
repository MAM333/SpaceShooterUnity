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
            // COLOR VERDE
            image.color = Color.green;
        }
        else
        {
            // COLOR ROJO
            image.color = Color.red;
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

        if (!upgraded)
        {
            // TIRA UN SONIDO MALO
        }
        else
        {
            // TIRA UN SONIDO DE CONFIRMACION
        }

        UpgradeCellsManager.instance.ActualizeAllCells();
    }

    private void ActualizeText()
    {
        int cost = Mejoras.instance.GetCostNextUpgrade(upgradeObject);
        text.text = upgradeObject.nombre + " " + cost.ToString();
    }
}
