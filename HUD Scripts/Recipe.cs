using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recipe {
    public int ID;
    public string name;
    public string slug;
    public Ingredient[] ingredients = new Ingredient[3];
    public GameObject itemModel;

    public class Ingredient {
        public int itemId { get; set; }
        public int count { get; set; } 
        public Ingredient(int itemId, int count) 
        {
            this.itemId = itemId;
            this.count = count;
        }
    }

    public Recipe(int ID, string name, string slug, int ingredient1, int ingred1count, int ingredient2, 
        int ingred2count, int ingredient3, int ingred3count) {
        this.ID = ID;
        this.name = name;
        this.slug = slug;

        this.ingredients[0] = new Ingredient(ingredient1, ingred1count);
        this.ingredients[1] = new Ingredient(ingredient2, ingred2count);
        this.ingredients[2] = new Ingredient(ingredient3, ingred3count);
    }

    public Recipe() {
        this.ID = -1;
    }
}
