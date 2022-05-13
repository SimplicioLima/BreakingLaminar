using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    [SerializeField] private List<ItemRequirement> requirements;
    public bool removeRequirementsOnPickup;

    public void OnHandlePickupItem()
    {
        if (MeetsRequirements())
        {
            if (removeRequirementsOnPickup)
            {
                RemoveRequirements();
            }

            InventorySystem.current.Add(referenceItem);
            Destroy(gameObject);
        }
    }

    public bool MeetsRequirements()
    {
        foreach (ItemRequirement requirement in requirements)
        {
            if(!requirement.HasRequirement()) { return false; }
        }
        return true;
    }

    public bool RemoveRequirements()
    {
        foreach (ItemRequirement requirement in requirements)
        {
            for (int i = 0; i < requirement.amount; i++)
            {
                if (requirement.itemData.displayName != "Single Emp")
                {
                    InventorySystem.current.Remove(requirement.itemData);
                }
            }
        }
        return true;
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
