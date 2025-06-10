using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NameOfItem", menuName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string description;
    public int id;

    public Sprite icon;
    public GameObject prefab;
}
