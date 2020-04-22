using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public List<Ingredient> mList, pList;
    public Dictionary<string, Ingredient> materialList = new Dictionary<string, Ingredient>();
    public Dictionary<string, Ingredient> potionList = new Dictionary<string, Ingredient>();
    void Start()
    {
        

    }

    public void LoadIngredients()
    {
        for (int i = 0; i < mList.Count; i++)
        {
            materialList.Add(mList[i].ingredientID, mList[i]);
            //Debug.Log("MaterialList NAMEID " + mList[i].ingredientID);
        }
        for (int i = 0; i < pList.Count; i++)
        {
            potionList.Add(pList[i].ingredientID, pList[i]);
        }
    }

    void AddIngredient(string ingredientID)
    {
        if (potionList.ContainsKey(ingredientID))
        {
            if (potionList[ingredientID].invAmount == -1)
            {
                potionList[ingredientID].invAmount++;
            }
            potionList[ingredientID].invAmount++;  
        }
        else if (materialList.ContainsKey(ingredientID))
        {
            if (materialList[ingredientID].invAmount == -1)
            {
                materialList[ingredientID].invAmount++;
            }
            materialList[ingredientID].invAmount++;
        }
    }

    void RemoveIngredient(string ingredientID)
    {
        if (potionList.ContainsKey(ingredientID))
        {
            potionList[ingredientID].invAmount--;
        }
        else if(materialList.ContainsKey(ingredientID))
        {
            materialList[ingredientID].invAmount--;
        }
        
    }

    public void CombineIngredients(string ingredientID1, string ingredientID2)
    {
        Debug.Log("COMBINE " + ingredientID1 + " AND " + ingredientID2 + materialList.ContainsKey(ingredientID1) + materialList.ContainsKey(ingredientID2));
        if(((materialList.ContainsKey(ingredientID1) && materialList.ContainsKey(ingredientID2)) && ((materialList[ingredientID1].invAmount > 0) && (materialList[ingredientID2].invAmount > 0))) ||
            ((materialList.ContainsKey(ingredientID1) && potionList.ContainsKey(ingredientID2)) && ((materialList[ingredientID1].invAmount > 0) && (potionList[ingredientID2].invAmount > 0))) ||
            ((potionList.ContainsKey(ingredientID1) && materialList.ContainsKey(ingredientID2)) && ((potionList[ingredientID1].invAmount > 0) && (materialList[ingredientID2].invAmount > 0))) ||
            ((potionList.ContainsKey(ingredientID1) && potionList.ContainsKey(ingredientID2)) && ((potionList[ingredientID1].invAmount > 0) && (potionList[ingredientID2].invAmount > 0))))
        {
            if(potionList.ContainsKey(ingredientID1 + ingredientID2))
            {
                AddIngredient(ingredientID1 + ingredientID2);
                RemoveIngredient(ingredientID1);
                RemoveIngredient(ingredientID2);
            }
            else if(potionList.ContainsKey(ingredientID2 + ingredientID1))
            {
                AddIngredient(ingredientID2 + ingredientID1);
                RemoveIngredient(ingredientID1);
                RemoveIngredient(ingredientID2);
            }
            else
            {
                //mistake
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
