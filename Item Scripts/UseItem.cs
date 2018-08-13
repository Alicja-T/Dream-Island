using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UseItem : MonoBehaviour, IPointerDownHandler
{
    public enum ItemType { Head, Eyes, Shirt, Pants, Shoes, Tool, Consumable, Material };
    public Item item;
    public GameObject mainInventory;
    public static Inventory inventory;
    // Use this for initialization
    void Start () {
        item = GetComponent<ItemData>().item;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            
            PlayerInventory playerInv = GameObject.Find("Player").GetComponent<PlayerInventory>();

            if (playerInv.inventory != null)
            {
                
                mainInventory = playerInv.inventory;
                if (mainInventory != null) {
                    inventory = mainInventory.GetComponent<Inventory>();
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData data) {
        
        if (inventory != null)
        {
           if (item.Type < 6)
            { //item is equippable 
                string s = Enum.GetName(typeof(ItemType), item.Type);
                if (inventory.equipSlots[item.Type] == null) {
                    GameObject inventoryItem = this.gameObject;
                    inventory.equipSlots[item.Type] = inventoryItem;
                    GameObject charSlot = inventory.charPanel.transform.GetChild(0).GetChild(item.Type).gameObject;
                    inventoryItem.transform.SetParent(charSlot.transform);
                    inventoryItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    Debug.Log("Item Equipped");
                }
                inventory.EquipItem(item);
            }
            if (item.Type == 6)
            { //item is consumable
                int amount;
                inventory.ConsumeItem(item);
                amount = inventory.RemoveItem(item);
                if (amount == 0) {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
