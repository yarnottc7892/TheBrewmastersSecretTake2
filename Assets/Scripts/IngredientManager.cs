using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    Dictionary<string, Ingredient> ingredientList = new Dictionary<string, Ingredient>();
    Dictionary<string, Ingredient> materialList = new Dictionary<string, Ingredient>();
    Dictionary<string, Ingredient> potionList = new Dictionary<string, Ingredient>();
    Dictionary<string, Ingredient> craftList = new Dictionary<string, Ingredient>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AddMaterial(string ingredientID)
    {
        try
        {
            materialList.Add(ingredientID, ingredientList[ingredientID]);
        }
        catch
        {
            materialList[ingredientID].invAmount++;
        }
    }

    void AddPotion(string ingredientID)
    {
        try
        {
            potionList.Add(ingredientID, ingredientList[ingredientID]);
        }
        catch
        {
            potionList[ingredientID].invAmount++;
        }
    }

    void RemoveMaterial(string ingredientID)
    {
        if(materialList[ingredientID].invAmount == 1)
        {
            materialList.Remove(ingredientID);
        }
        else
        {
            materialList[ingredientID].invAmount--;
        }
        
    }

    void CombineIngredients(string ingredientID1, string ingredientID2)
    {
        if((materialList.ContainsKey(ingredientID1) || potionList.ContainsKey(ingredientID1)) && (materialList.ContainsKey(ingredientID2) || potionList.ContainsKey(ingredientID2)))
        {
            if(craftList.ContainsKey(ingredientID1 + ingredientID2))
            {
                AddPotion(ingredientID1 + ingredientID2);
                RemoveMaterial(ingredientID1);
                RemoveMaterial(ingredientID2);
            }
            else if(craftList.ContainsKey(ingredientID2 + ingredientID1))
            {
                AddPotion(ingredientID2 + ingredientID1);
                RemoveMaterial(ingredientID1);
                RemoveMaterial(ingredientID2);
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
