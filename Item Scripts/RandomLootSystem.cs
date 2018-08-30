using UnityEngine;
using System.Collections;

public class RandomLootSystem : MonoBehaviour {

    public int itemsNumber = 100;
    static ItemDataBase itemList;
	void Start () {
        int counter = 0;
        itemList = GameObject.Find("Inventory").GetComponent<ItemDataBase>();
        while (counter < itemsNumber){
            counter++;
            int randomNumber = Random.Range(0, itemList.database.Count);
            Terrain terrain = Terrain.activeTerrain;
            float x = Random.Range(320, 1700);
            float z = Random.Range(300, 1760);
            GameObject randomItem = (GameObject)Instantiate(itemList.database[randomNumber].itemModel);
            ItemButton button = randomItem.AddComponent<ItemButton>();
            button.SetItemID(itemList.database[randomNumber].ID);
            button.SetItemName(itemList.database[randomNumber].Title);
            randomItem.transform.SetParent( terrain.transform );
            Vector3 pos = new Vector3(x, 0, z); 
            pos.y = terrain.SampleHeight(pos);
            randomItem.transform.position = new Vector3(x, pos.y, z);
            
        }
     }

	
	
}
