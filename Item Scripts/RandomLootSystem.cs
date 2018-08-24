using UnityEngine;
using System.Collections;

public class RandomLootSystem : MonoBehaviour {

    public int itemsNumber = 100;
    static ItemDataBase itemList;
	void Start () {
        int counter = 0;
        itemList = GetComponent<ItemDataBase>();
        while (counter < itemsNumber){
            counter++;
            int randomNumber = Random.Range(0, itemList.database.Count);
            Terrain terrain = Terrain.activeTerrain;
            float x = Random.Range(320, 1700);
            float z = Random.Range(300, 1760);

            print(itemList.database[0].Title);
            GameObject randomItem = (GameObject)Instantiate(itemList.database[randomNumber].itemModel);
            ItemButton button = randomItem.AddComponent<ItemButton>();
            button.SetItemID(itemList.database[randomNumber].ID);
            button.SetItemName(itemList.database[randomNumber].Title);
            randomItem.transform.SetParent( terrain.transform );
            transform.position = new Vector3(x, 0, z);
            Vector3 pos = transform.position;
            pos.y = terrain.SampleHeight(transform.position);
            randomItem.transform.localPosition = new Vector3(x, pos.y, z);
            
        }
     }

	
	
}
