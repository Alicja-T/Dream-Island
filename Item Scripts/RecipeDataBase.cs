using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class RecipeDataBase : MonoBehaviour {

    public List<Recipe> database = new List<Recipe>();
    private JsonData recipeData;
    void Start()
    {
        recipeData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Recipes.json"));
        ConstructRecipeDatabase();
    }

    public Recipe getRecipeByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
                return database[i];
        }
        return null;
    }

    void ConstructRecipeDatabase()
    {
        for (int i = 0; i < recipeData.Count; i++)
        {
            database.Add(new Recipe((int)recipeData[i]["id"], recipeData[i]["name"].ToString(), recipeData[i]["slug"].ToString(),
                (int)recipeData[i]["ingredient1"], (int)recipeData[i]["ingred1count"], (int)recipeData[i]["ingredient2"],
                 (int)recipeData[i]["ingred2count"], (int)recipeData[i]["ingredient3"], (int)recipeData[i]["ingred3count"]));
        }
    }

}
