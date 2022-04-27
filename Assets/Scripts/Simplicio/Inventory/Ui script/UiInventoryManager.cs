using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UiInventoryManager : MonoBehaviour
{
    public bool inDebug = true;
    private bool _isActive = false;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _invCanvas;
    [SerializeField] private GameObject _parentSlot;

    //Ui info
    private int invSlots;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActivateInv();
    }
    public async void ActivateInv()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (Transform t in transform)
            {
                if (inDebug) Debug.Log(t.name);
                Destroy(t.gameObject);
            }
            if (!_isActive)
            {
                _invCanvas.SetActive(true);
                _isActive = true;
                await Task.Delay(500);
                UpdateUi();
            }
            else
            {
                _isActive = false;
                _invCanvas.SetActive(false);
            }
        }
    }

    void UpdateUi()
    {
        foreach (var item in InventorySystem.current.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        if (inDebug) Debug.Log(item.data.name);
        GameObject obj = Instantiate(_slotPrefab);
        obj.transform.SetParent(_parentSlot.transform, false);

        InventorySlot slot = obj.GetComponent<InventorySlot>();
        slot.Set(item);
    }
}
