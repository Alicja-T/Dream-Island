using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;
    GameObject recipePanel;
    ItemDataBase database;
    RecipeDataBase recipes;
    public GameObject craftingPanel;
    public GameObject recipeSlot;
    public GameObject charPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    
   
    bool inventoryClosed = true;
    bool charPanelClosed = true;
    bool craftPanelClosed = true;
    
     //methods for consuming items and equipping items
    public delegate void ItemDelegate(Item item);
    public static event ItemDelegate ItemConsumed;
    public static event ItemDelegate ItemEquip;
    public static event ItemDelegate UnEquipItem;

    int slotNumber;
    int recipesNumber;
    public List<Item> items = new List<Item>();
    public List<GameObject> recipesFound = new List<GameObject>();
    public List<GameObject> mainSlots = new List<GameObject>();
    public GameObject[] equipSlots; 

    void Awake()
    {
        equipSlots = new GameObject[6];
        database = GetComponent<ItemDataBase>();
        recipes = GetComponent<RecipeDataBase>();
        slotNumber = 60;
        recipesNumber = 2;
        inventoryPanel = GameObject.Find("Inventory Panel");
        charPanel = GameObject.Find("EquipmentPanel");
        craftingPanel = GameObject.Find("CraftingPanel");
        recipePanel = GameObject.Find("RecipePanel");
        craftingPanel.SetActive(false);
        charPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;
        for (int i = 0; i < slotNumber; i++)
        {
            items.Add(new Item());
            mainSlots.Add(Instantiate(inventorySlot));
            mainSlots[i].transform.SetParent(slotPanel.transform);
        }
        for (int i = 0; i < recipesNumber; i++) {
            recipesFound.Add(Instantiate(recipeSlot));
            recipesFound[i].transform.Translate( Vector3.zero );
            recipesFound[i].transform.SetParent(recipePanel.transform);
            //AddRecipe(i);
        }

             
    }

    public int RemoveItem(Item item) {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID )
            {
                ItemData data = mainSlots[i].transform.GetChild(0).GetComponent<ItemData>();
                if (data.amount > 1)
                {
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    return data.amount;
                }
                else {
                    data.amount = 0;
                    items.RemoveAt(i);
                    return data.amount;
                }
            }

        }
        return -1;
    }

    public void AddRecipe(int id) {
        
                  
            Recipe recipeToAdd = recipes.getRecipeByID(id);
            Text recipeName = recipesFound[id].transform.GetChild(1).GetComponent<Text>();
            recipeName.text = recipeToAdd.name;
                         
        
    }

    public void AddItemForCrafting(int id) {
        for (int i = 0; i < recipes.database.Count; i++) {
            for (int j = 0; j < 3; j++) {
                if (recipes.database[i].ingredients[j].itemId == id) {
                    Item ingredient = database.getItemByID(recipes.database[i].ingredients[j].itemId);
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.transform.SetParent(recipesFound[i].transform.GetChild(j+2));
                    itemObj.GetComponent<RectTransform>().localPosition = Vector3.zero; //this is necessary to put sprite in correct position
                    itemObj.GetComponent<Image>().sprite = ingredient.Sprite;

                }
            }
        }

    }

    public void CreateObject(int id) {
        Terrain terrain = Terrain.activeTerrain;
        GameObject itemModel = Resources.Load<GameObject>("Models/Items/Campfire");
        GameObject newItem = (GameObject)Instantiate(itemModel);
        newItem.transform.SetParent(terrain.transform);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 pos = player.transform.position;
        pos.y = terrain.SampleHeight(player.transform.position) + (float)0.5;
        newItem.transform.localPosition = new Vector3(pos.x + 2, pos.y, pos.z);
    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.getItemByID(id);
        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = mainSlots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }

            }

        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.transform.SetParent(mainSlots[i].transform);
                    itemObj.GetComponent<RectTransform>().localPosition = Vector3.zero; //this is necessary to put sprite in correct position
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;
                    ItemData data = mainSlots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.item = itemToAdd;
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }

        AddItemForCrafting(id);
    }

    bool CheckIfItemIsInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
                return true;
        }
        return false;
    }
   
    public bool isInvClosed() {
        return inventoryClosed;
    }

    public bool isCharPanelClosed()
    {
        return charPanelClosed;
    }

    public bool isCraftPanelClosed() {
        return craftPanelClosed;
    }

    public void openCharPanel() {
        if ( charPanelClosed ){
            charPanel.SetActive(true);
            charPanelClosed = false;
        }
    }

    public void closeCharPanel() {

        charPanel.SetActive(false);
        charPanelClosed = true;
    }

    public void openCraftPanel() {
        if (craftPanelClosed)
        {
            craftingPanel.SetActive(true);
            craftPanelClosed = false;
        }
    }

    public void closeCraftPanel() {

        craftingPanel.SetActive(false);
        craftPanelClosed = true;
    }

    public void openInventory()
    {

        if ( inventoryClosed )
        {
            inventoryPanel.SetActive(true);
            inventoryClosed = false;
        }
    }

    public void closeInventory()
    {
        inventoryPanel.SetActive(false);
        inventoryClosed = true;
    }


    public void ConsumeItem(Item item)
    {
        if (ItemConsumed != null)
            ItemConsumed(item);
    }

    public void EquipItem(Item item)
    {
        if (ItemEquip != null)
            ItemEquip(item);
    }

    public void UnEquipItem1(Item item)
    {
        if (UnEquipItem != null)
            UnEquipItem(item);
    }
}
