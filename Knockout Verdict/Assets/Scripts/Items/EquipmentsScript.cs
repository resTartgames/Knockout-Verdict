using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentScript : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField] EquipmentType equipmentType;

    [SerializeField]
    private Sprite itemSprite;

    [TextArea][SerializeField] private string itemDescription;


    [SerializeField]
    private InventoryManagerScript inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("PauseCanvas").GetComponent<InventoryManagerScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") { 
            bool inventoryHasSpace = inventoryManager.AddItem(itemName, itemSprite, itemDescription, equipmentType);
            if (inventoryHasSpace) { Destroy(gameObject); }
            
        }

    }
}
