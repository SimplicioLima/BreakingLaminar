using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _name;
    [SerializeField] private GameObject _stackObj;
    [SerializeField] private Text _stackAmout;

    public void Set(InventoryItem item)
    {
        _icon.sprite = item.data.icon;
        _name.text = item.data.displayName;
        if(item.stackSize <= 1)
        {
            _stackObj.SetActive(false);
            return;
        }

        _stackAmout.text = item.stackSize.ToString();
    }
}
