using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool inDebug = true;
    public static bool _isActive = false;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _invCanvas;
    [SerializeField] private GameObject _parentSlot;

    private void Start()
    {
        InventorySystem.current.onInventoryChangedEvent += OnUpdateInventory;
        _invCanvas.SetActive(false);
    }

    private void Update()
    {
        if (_isActive)
        {
            OnUpdateInventory();
        }
        
    }

    private void OnUpdateInventory()
    {
        
        foreach (Transform t in _parentSlot.transform)
        {
            if (inDebug) Debug.Log(t.name);
            Destroy(t.gameObject);
        }
        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach (var item in InventorySystem.current.inventory)
        {
            if (inDebug) Debug.Log(item.data.name);
        }
        foreach (InventoryItem item in InventorySystem.current.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(_slotPrefab);
        obj.transform.SetParent(_parentSlot.transform, false);

        InventorySlot slot = obj.GetComponent<InventorySlot>();
        slot.Set(item);
    }
}
