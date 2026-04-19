using System.Collections.Generic;
using UnityEngine;

public class UpgradeCellsManager : MonoBehaviour
{
    public static UpgradeCellsManager instance;

    public GameObject cellObject;
    public float initX = -300f, initY = 140f;
    public float offsetX = 115f;
    public float offsetY = -50f;
    public int rowsMaxCells = 6;

    private List<UpgradeCell> cells = new List<UpgradeCell>();
    private float actX = 0, actY = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        List<UpgradeObject> upgradesInfo = GetOrderedUpgrades();

        actX = initX;
        actY = initY;
        int i = 0;
        foreach (UpgradeObject upgradeObject in upgradesInfo)
        {
            if (!upgradeObject.active) continue;

            GameObject cell = Instantiate(cellObject, transform);
            RectTransform rectTransform = cell.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(actX, actY);

            // Inicializacion de upgrade cell
            UpgradeCell upgCell = cell.GetComponent<UpgradeCell>();
            upgCell.SetUpgradeObject(upgradeObject);
            upgCell.ChangeBoxColor();

            cells.Add(upgCell);
            
            i++;
            if (i >= rowsMaxCells)
            {
                i = 0;
                actX = initX;
                actY += offsetY;
            }
            else
            {
                actX += offsetX;
            }
        }
    }

    private List<UpgradeObject> GetOrderedUpgrades()
    {
        List<UpgradeObject> upgradesInfo = Mejoras.instance.GetUpgradesInfo();

        List<UpgradeObject> ret = new List<UpgradeObject>();
        for (int i = 0; i < upgradesInfo.Count; ++i)
        {
            float costMinAct = 99999;
            UpgradeObject upgAct = null;
            foreach (UpgradeObject upg in upgradesInfo)
            {
                if (!upg.active || upg.notImplemented) continue;

                if (upg.costs[1] < costMinAct)
                {
                    bool encountered = false;
                    foreach (UpgradeObject retUpg in ret)
                    {
                        if (retUpg.id == upg.id)
                        {
                            encountered = true;
                            break;
                        }
                    }

                    if (!encountered)
                    {
                        costMinAct = upg.costs[1];
                        upgAct = upg;
                    }
                }
            }

            if (upgAct != null) ret.Add(upgAct);
            else break;
        }

        return ret;
    }

    public void ActualizeAllCells()
    {
        foreach (UpgradeCell cell in cells)
        {
            cell.ChangeBoxColor();
        }
    }
}
