using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mission Inventory Item Data")]
public class MissonInventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public GameObject prefab;
    public bool canGrab;
    public MissonInventoryItemData previous;
    public MissonInventoryItemData next;
}
