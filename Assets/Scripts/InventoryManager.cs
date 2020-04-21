using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    IngredientManager ingredientManager;
    public List<InventorySlot> invSlot;
    List<GameObject> invDragDrop = new List<GameObject>();
    public GameObject dragDropPrefab;
    public Transform dragParent;
    // Start is called before the first frame update
    void Start()
    {
        ingredientManager = GetComponent<IngredientManager>();
        ingredientManager.LoadIngredients();
        Canvas canvas = GetComponentInParent<Canvas>();
        //invDragDrop = new List<InventoryDragDrop>(invSlot.Count);
        for (int i = 0; i < invSlot.Count; i++)
        {
            GameObject temp = Instantiate(dragDropPrefab, dragParent);
            temp.GetComponent<InventoryDragDrop>().canvas = canvas;
            invDragDrop.Add(temp);
            
        }
        Debug.Log(invDragDrop.Count);
        Debug.Log(invSlot[0].rT);
        for(int i=0; i<invSlot.Count; i++)
        {
            invDragDrop[i].GetComponent<InventoryDragDrop>().rT.anchoredPosition = invSlot[i].rT.anchoredPosition;
        }
        LoadMaterials();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadMaterials()
    {
        int i = 0;
        Debug.Log("MaterialList COunt in inv manager " + ingredientManager.materialList.Count);
        foreach (KeyValuePair<string, Ingredient> entry in ingredientManager.materialList)
        {
            invDragDrop[i].GetComponent<IngredientDisplay>().ingredient = entry.Value;
            invDragDrop[i].GetComponent<IngredientDisplay>().SetImage();
            i++;
        }
        print(i);
    }

    void LoadPotions()
    {
        int i = 0;

        foreach (KeyValuePair<string, Ingredient> entry in ingredientManager.potionList)
        {
            invDragDrop[i].GetComponent<IngredientDisplay>().ingredient = entry.Value;
            invDragDrop[i].GetComponent<IngredientDisplay>().image.sprite = entry.Value.sprite;
            i++;
        }
    }
}
