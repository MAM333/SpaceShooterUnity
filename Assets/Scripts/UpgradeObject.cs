using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeObject")]
public class UpgradeObject : ScriptableObject
{
    public string id;
    public string nombre;
    public string description;

    public int[] costs;
    public float[] values;

    public bool active = true;
    public bool notImplemented;
}
