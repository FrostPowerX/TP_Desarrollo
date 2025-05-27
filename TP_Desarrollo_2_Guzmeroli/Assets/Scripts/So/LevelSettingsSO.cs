using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettingsSO", menuName = "LevelSettingsSO")]
public class LevelSettingsSO : ScriptableObject
{
    public string diffName;

    public int minCountOrder;
    public int maxCountOrder;

    public int timePerOrder;
    public int totalOrders;

    public float timeLimit;

    public Vector3 playerPos;
}
