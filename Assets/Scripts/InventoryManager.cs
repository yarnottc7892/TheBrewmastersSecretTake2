using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    IngredientManager ingredientManager;
    public List<InventorySlot> invSlot;
    public InventorySlot craftSlot1, craftSlot2, productSlot;
    private Ingredient craftIngredient1 = null, craftIngredient2 = null;
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
            invDragDrop[i].GetComponent<InventoryDragDrop>().invSlot = invSlot[i];
            invDragDrop[i].GetComponent<InventoryDragDrop>().craftSlot1 = craftSlot1;
            invDragDrop[i].GetComponent<InventoryDragDrop>().craftSlot2 = craftSlot2;
        }
        LoadMaterials();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMaterials()
    {
        Debug.Log("LOADMATS");
        int i = 0;
        Debug.Log("MaterialList COunt in inv manager " + ingredientManager.materialList.Count);
        foreach (KeyValuePair<string, Ingredient> entry in ingredientManager.materialList)
        {
            invDragDrop[i].GetComponent<IngredientDisplay>().ingredient = entry.Value;
            invDragDrop[i].GetComponent<IngredientDisplay>().SetImage();
            i++;
        }
        //print(i);
    }

    public void LoadPotions()
    {
        Debug.Log("LOADPOTS");
        int i = 0;

        foreach (KeyValuePair<string, Ingredient> entry in ingredientManager.potionList)
        {
            invDragDrop[i].GetComponent<IngredientDisplay>().ingredient = entry.Value;
            invDragDrop[i].GetComponent<IngredientDisplay>().image.sprite = entry.Value.sprite;
            i++;
        }
    }

    public void CraftSlotted(Ingredient i, InventorySlot iS)
    {
        if(iS == craftSlot1)
        {
            craftIngredient1 = i;
        }
        else if(iS == craftSlot2)
        {
            craftIngredient2 = i;
        }
        if(craftIngredient1 != null && craftIngredient2 != null)
        {
            ingredientManager.CombineIngredients(craftIngredient1.ingredientID, craftIngredient2.ingredientID);
            //create product drag drop

        }
    }

    public void CraftUnslotted(InventorySlot iS)
    {
        if (iS == craftSlot1)
        {
            craftIngredient1 = null;
        }
        else if (iS == craftSlot2)
        {
            craftIngredient2 = null;
        }
    }
}
