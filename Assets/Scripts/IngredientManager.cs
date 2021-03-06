﻿using System.Collections;
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

    public void ResetAll()
    {
        foreach (KeyValuePair<string, Ingredient> pair in materialList)
        {
            pair.Value.invAmount = -1;
        }

        foreach (KeyValuePair<string, Ingredient> pair in potionList)
        {
            pair.Value.invAmount = -1;
        }
    }

    private void Update()
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

    public void AddIngredient(string ingredientID)
    {
        if (potionList.ContainsKey(ingredientID))
        {
            if (potionList[ingredientID].invAmount == -1)
            {
                potionList[ingredientID].invAmount++;
            }
            potionList[ingredientID].invAmount++;

            GameManager.AddCard(potionList[ingredientID].name);

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

            GameManager.RemoveCard(potionList[ingredientID].name);
        }
        else if(materialList.ContainsKey(ingredientID))
        {
            materialList[ingredientID].invAmount--;
        }
        //LoadIngredients();
    }

    public string CombineIngredients(string ingredientID1, string ingredientID2)
    {
        Debug.Log("COMBINE " + ingredientID1 + " AND " + ingredientID2 + materialList.ContainsKey(ingredientID1) + materialList.ContainsKey(ingredientID2));
        if(((materialList.ContainsKey(ingredientID1) && materialList.ContainsKey(ingredientID2)) && ((materialList[ingredientID1].invAmount > 0) && (materialList[ingredientID2].invAmount > 0))) ||
            ((materialList.ContainsKey(ingredientID1) && potionList.ContainsKey(ingredientID2)) && ((materialList[ingredientID1].invAmount > 0) && (potionList[ingredientID2].invAmount > 0))) ||
            ((potionList.ContainsKey(ingredientID1) && materialList.ContainsKey(ingredientID2)) && ((potionList[ingredientID1].invAmount > 0) && (materialList[ingredientID2].invAmount > 0))) ||
            ((potionList.ContainsKey(ingredientID1) && potionList.ContainsKey(ingredientID2)) && ((potionList[ingredientID1].invAmount > 0) && (potionList[ingredientID2].invAmount > 0))))
        {
            if(potionList.ContainsKey(ingredientID1 + ingredientID2))
            {
                print("COMBO12");
                AddIngredient(ingredientID1 + ingredientID2);
                RemoveIngredient(ingredientID1);
                RemoveIngredient(ingredientID2);
                return ingredientID1 + ingredientID2;
            }
            else if(potionList.ContainsKey(ingredientID2 + ingredientID1))
            {
                print("COMBO21");
                AddIngredient(ingredientID2 + ingredientID1);
                RemoveIngredient(ingredientID1);
                RemoveIngredient(ingredientID2);
                return ingredientID2 + ingredientID1;
            }
            else
            {
                //mistake
            }
            
        }
        return "";
    }

    public string FindComboID(string ingredientID1, string ingredientID2)
    {
        //Debug.Log("COMBINE " + ingredientID1 + " AND " + ingredientID2 + materialList.ContainsKey(ingredientID1) + materialList.ContainsKey(ingredientID2));
        if (((materialList.ContainsKey(ingredientID1) && materialList.ContainsKey(ingredientID2)) && ((materialList[ingredientID1].invAmount > 0) && (materialList[ingredientID2].invAmount > 0))) ||
            ((materialList.ContainsKey(ingredientID1) && potionList.ContainsKey(ingredientID2)) && ((materialList[ingredientID1].invAmount > 0) && (potionList[ingredientID2].invAmount > 0))) ||
            ((potionList.ContainsKey(ingredientID1) && materialList.ContainsKey(ingredientID2)) && ((potionList[ingredientID1].invAmount > 0) && (materialList[ingredientID2].invAmount > 0))) ||
            ((potionList.ContainsKey(ingredientID1) && potionList.ContainsKey(ingredientID2)) && ((potionList[ingredientID1].invAmount > 0) && (potionList[ingredientID2].invAmount > 0))))
        {
            if (potionList.ContainsKey(ingredientID1 + ingredientID2))
            {
                
                return ingredientID1 + ingredientID2;
            }
            else if (potionList.ContainsKey(ingredientID2 + ingredientID1))
            {
                
                return ingredientID2 + ingredientID1;
            }
            else
            {
                //mistake
            }

        }
        //print("" + materialList.ContainsKey(ingredientID1) + materialList.ContainsKey(ingredientID2) + (materialList[ingredientID1].invAmount > 0) + materialList[ingredientID1].invAmount + (materialList[ingredientID2].invAmount > 0) + materialList[ingredientID2].invAmount);
        return "";
    }
}
