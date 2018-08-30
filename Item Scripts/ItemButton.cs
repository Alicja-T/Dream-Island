using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;



public class ItemButton : MonoBehaviour {
    
    private int itemID;
    private string itemName;
    public Color highlightColor;
    private Color itemColor;
    public Text itemText;
    
    
    void Start () {

                     
	}
        
   
    

  public void ShowItemName() {
       
        Vector3 TextLocation = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        TextLocation.y = (Screen.height - TextLocation.y) - 30;
               
   }

    public void SetItemID(int id) {
        this.itemID = id;
    }

    public void SetItemName(string name) {
        this.itemName = name;
    }

    public int GetItemID() {
        return itemID;
    }

    public string GetItemName() {
        return itemName;
    }
}
