﻿using UnityEngine;
using System.Collections;

public class ItemData : MonoBehaviour {
    public Item item;
    public int amount;

    void Update() {

        GetComponent<UseItem>().item = item;
    }


}
