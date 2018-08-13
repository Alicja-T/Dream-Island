using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;



public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler  {
    
    private int itemID;
    private string itemName;
    Renderer renderer;
    public Color highlightColor;
    private Color itemColor;
    private Inventory _inventory;
    private GameObject _player;
    bool showGui = false;
    public GUIStyle myStyle;
    
    void Start () {

        _player = GameObject.Find("Player");
        if (_player != null)
        {
           _inventory = _player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
        }
        renderer = GetComponent<MeshRenderer>();
        itemColor = renderer.material.GetColor("_Color");
              
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        showGui = true;
         renderer.material.color = highlightColor;
          }

    public void OnPointerExit(PointerEventData eventData)
    {
        showGui = false;
         renderer.material.color = itemColor;
    }

   void OnGUI() {
        
       if (showGui) {
           ShowItemName();
       }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _inventory.AddItem(itemID);
        Destroy(this.gameObject);
    }

   void ShowItemName() {
        GUIStyle myStyle = new GUIStyle();
        myStyle.richText = true;
        Vector3 TextLocation = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        TextLocation.y = (Screen.height - TextLocation.y) - 30;
        GUI.Label(new Rect(TextLocation.x, TextLocation.y, 100, 50), itemName, myStyle );
        
   }

    public void SetItemID(int id) {
        this.itemID = id;
    }

    public void SetItemName(string name) {
        this.itemName = name;
    }
}
