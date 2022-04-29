using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool inDebug = false;
    public static bool _isActive = false;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _invCanvas;
    [SerializeField] private GameObject _parentSlot;

    //public static List<InventoryItem> slotItem;

    private void Start()
    {
        InventorySystem.current.onInventoryChangedEvent += OnUpdateInventory;
        
        _invCanvas.SetActive(false);
    }

    private void Update()
    {
        if (_isActive || Input.GetKeyDown(KeyCode.Q))
        {
            OnUpdateInventory();
        }
    }

    private async void UpdateInv()
    {
        if (InventorySystem.current.slotItem.Count > 0)
        {
            await Task.Delay(500);
            OnUpdateInventory();
        }
    }

    private void OnUpdateInventory()
    {
        foreach (Transform t in _parentSlot.transform)
        {
            Destroy(t.gameObject);
        }
        InventorySystem.current.slotItem.Clear();
        //InventorySystem.current.slotItem = new List<InventoryItem>();
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
        if(item.data.canThrow == true) InventorySystem.current.slotItem.Add(item);
    }
}
