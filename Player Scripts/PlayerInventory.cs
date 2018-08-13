using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInventory : MonoBehaviour {

    public enum ItemType { Head, Eyes, Shirt, Pants, Shoes, Tool, Consumable, Material };
    public GameObject inventory;
    public GameObject StatsBar;
    private Inventory mainInventory; 
    private InputManager inputManagerDatabase;
    float lastDrink = 0; //time from last eating
    float lastFood = 0; //time from last drink
    float currentTime = 0;
    float previousTime = 0;
    public GameTime gameTime;

    
    Image energyImage;
    Image hydrationImage;
    Image staminaImage;

    float energy = 80;
    float hydration = 80;
    float stamina = 80;
    float maxEnergy = 100;
    float maxHydration = 100;
    float maxStamina = 100;

    public void OnEnable()
    {
        Inventory.ItemEquip += OnCharacter;
        Inventory.ItemConsumed += ConsumeItem;
    }

    public void OnDisable()
    {
        Inventory.ItemEquip -= OnCharacter;
        Inventory.ItemConsumed -= ConsumeItem;
    }

    public float GetStamina() {
        return stamina;
    }

    void OnCharacter(Item item) {
        //Debug.Log("Item Equipped");

    }

    void ConsumeItem(Item item) {
        //Debug.Log("Item Consumed");
        hydration = hydration + item.Thirst;
        stamina = stamina + item.Protein;
        energy = energy + item.Energy;
        UpdateAll();
    }

    void Start () {

        if (StatsBar != null) {
           
           energyImage = StatsBar.transform.GetChild(0).GetChild(0).GetComponent<Image>();
           hydrationImage = StatsBar.transform.GetChild(0).GetChild(2).GetComponent<Image>();
           staminaImage = StatsBar.transform.GetChild(0).GetChild(4).GetComponent<Image>();
           UpdateEnergy();
           UpdateHydration();
           UpdateStamina();
        }

        if (inventory != null)
        {
            mainInventory = inventory.GetComponent<Inventory>();
            
        }   

        if (inputManagerDatabase == null)
            inputManagerDatabase = (InputManager)Resources.Load("InputManager");
    }

    void UpdateEnergy() {
        float fillAmount = energy / maxEnergy;
        energyImage.fillAmount = fillAmount;

    }

    void UpdateHydration() {
        float fillAmount = hydration / maxHydration;
        hydrationImage.fillAmount = fillAmount;
    }

   
    void UpdateStamina() {
        float fillAmount = stamina / maxStamina;
        staminaImage.fillAmount = fillAmount;
    }

    void UpdateAll() {//update all stats
        UpdateEnergy();
        UpdateHydration();
        UpdateStamina();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
        {
            if ( mainInventory.isInvClosed() )
            {
                mainInventory.openInventory();
            }
            else
            {
                mainInventory.closeInventory();
            }
        }
        if (Input.GetKeyDown(inputManagerDatabase.CharPanelKeyCode)) {
            if (mainInventory.isCharPanelClosed())
            {
                mainInventory.openCharPanel();
            }
            else
            {
                mainInventory.closeCharPanel();
            }

        }
        if (Input.GetKeyDown(inputManagerDatabase.CraftPanelKeyCode)) {
            if (mainInventory.isCraftPanelClosed())
            {

                mainInventory.openCraftPanel();
            }
            else {
                mainInventory.closeCraftPanel();
            }
        }

        currentTime = gameTime.GetTime();
        if ((currentTime-previousTime) > 60) {
            previousTime = currentTime;
            if (hydration > 30)
            {
                energy = energy - ((currentTime) / 60);
                hydration = hydration - ((currentTime) / 30);
                //Debug.Log(currentTime);
                stamina = stamina - ((currentTime) / 60);
                UpdateAll();
            }
        }
    
    }
}
