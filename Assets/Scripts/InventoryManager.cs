using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    IngredientManager ingredientManager;
    public List<InventorySlot> invSlot;
    public InventorySlot craftSlot1, craftSlot2, productSlot;
    private InventoryDragDrop craftDD1, craftDD2;
    private Ingredient craftIngredient1 = null, craftIngredient2 = null;
    List<GameObject> invMaterialDragDrop = new List<GameObject>();
    List<GameObject> invPotionDragDrop = new List<GameObject>();
    public GameObject dragDropPrefab;
    public Transform dragParent;
    public bool isOnMatTab = true;
    bool canCraft = false;
    int craftIndex = -1;
    Sprite productSlotDefault;
    // Start is called before the first frame update
    void Awake()
    {
        productSlotDefault = productSlot.GetComponent<Image>().sprite;
        ingredientManager = GetComponent<IngredientManager>();
        print(ingredientManager);
        ingredientManager.LoadIngredients();
        Canvas canvas = GetComponentInParent<Canvas>();
        //invDragDrop = new List<InventoryDragDrop>(invSlot.Count);
        for (int i = 0; i < invSlot.Count; i++)
        {
            GameObject temp = Instantiate(dragDropPrefab, dragParent);
            temp.GetComponent<InventoryDragDrop>().canvas = canvas;
            invMaterialDragDrop.Add(temp);
            temp = Instantiate(dragDropPrefab, dragParent);
            temp.GetComponent<InventoryDragDrop>().canvas = canvas;
            invPotionDragDrop.Add(temp);

        }
        //Debug.Log(invDragDrop.Count);
        //Debug.Log(invSlot[0].rT);
        for(int i=0; i<invSlot.Count; i++)
        {
            var iMDD = invMaterialDragDrop[i].GetComponent<InventoryDragDrop>();
            var iPDD = invPotionDragDrop[i].GetComponent<InventoryDragDrop>();

            iMDD.rT.anchoredPosition = invSlot[i].rT.anchoredPosition;
            iMDD.invSlot = invSlot[i];
            iMDD.currSlot = invSlot[i];
            iMDD.craftSlot1 = craftSlot1;
            iMDD.craftSlot2 = craftSlot2;
            iMDD.productSlot = productSlot;
            invMaterialDragDrop[i].SetActive(false);

            iPDD.rT.anchoredPosition = invSlot[i].rT.anchoredPosition;
            iPDD.invSlot = invSlot[i];
            iPDD.currSlot = invSlot[i];
            iPDD.craftSlot1 = craftSlot1;
            iPDD.craftSlot2 = craftSlot2;
            iPDD.productSlot = productSlot;
            invPotionDragDrop[i].SetActive(false);
        }
        LoadPotions();
        LoadMaterials();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMaterials()
    {
        isOnMatTab = true;
        //Debug.Log("LOADMATS");
        int i = 0;
        print(ingredientManager);
        Debug.Log("MaterialList Count in inv manager " + ingredientManager.materialList.Count);
        foreach (KeyValuePair<string, Ingredient> entry in ingredientManager.materialList)
        {
            invMaterialDragDrop[i].SetActive(true);
            invMaterialDragDrop[i].GetComponent<IngredientDisplay>().ingredient = entry.Value;
            invMaterialDragDrop[i].GetComponent<IngredientDisplay>().SetImage();
            
            i++;
        }

        print("MATS LOADED: " + i);

        for(int j = i; j < invSlot.Count; j++)
        {
            invMaterialDragDrop[j].SetActive(true);
        }

        i = 0;

        foreach (KeyValuePair<string, Ingredient> entry in ingredientManager.potionList)
        {
            if(invPotionDragDrop[i].GetComponent<InventoryDragDrop>().currSlot != craftSlot1 && invPotionDragDrop[i].GetComponent<InventoryDragDrop>().currSlot != craftSlot2 && invPotionDragDrop[i].GetComponent<InventoryDragDrop>().currSlot != productSlot)
            {
                invPotionDragDrop[i].SetActive(false);
            }
            
            i++;
        }

        for (int j = i; j < invSlot.Count; j++)
        {
            invPotionDragDrop[j].SetActive(false);
        }
        //print(i);
    }

    public void LoadPotions()
    {
        isOnMatTab = false;
        //Debug.Log("LOADPOTS");
        int i = 0;

        foreach (KeyValuePair<string, Ingredient> entry in ingredientManager.potionList)
        {
            invPotionDragDrop[i].SetActive(true);
            invPotionDragDrop[i].GetComponent<IngredientDisplay>().ingredient = entry.Value;
            invPotionDragDrop[i].GetComponent<IngredientDisplay>().SetImage();

            i++;
        }

        for (int j = i; j < invSlot.Count; j++)
        {
            invPotionDragDrop[j].SetActive(true);
        }

        i = 0;
        foreach (KeyValuePair<string, Ingredient> entry in ingredientManager.materialList)
        {
            if (invMaterialDragDrop[i].GetComponent<InventoryDragDrop>().currSlot != craftSlot1 && invMaterialDragDrop[i].GetComponent<InventoryDragDrop>().currSlot != craftSlot2 && invMaterialDragDrop[i].GetComponent<InventoryDragDrop>().currSlot != productSlot)
            {
                invMaterialDragDrop[i].SetActive(false);
            }
                
            i++;
        }

        for (int j = i; j < invSlot.Count; j++)
        {
            invMaterialDragDrop[j].SetActive(false);
        }
    }

    public void CraftSlotted(Ingredient i, InventorySlot iS, InventoryDragDrop iDD)
    {
        if(iS == craftSlot1)
        {
            craftIngredient1 = i;
            craftDD1 = iDD;
        }
        else if(iS == craftSlot2)
        {
            craftIngredient2 = i;
            craftDD2 = iDD;
        }
        if(craftIngredient1 != null && craftIngredient2 != null)
        {
            string craftId = ingredientManager.FindComboID(craftIngredient1.ingredientID, craftIngredient2.ingredientID);
            print("CraftID: " + craftId);
            if(!craftId.Equals(""))
            {
                print("cancrafttrue");
                canCraft = true;
                craftIndex = 0;

                foreach (KeyValuePair<string, Ingredient> entry in ingredientManager.potionList)
                {
                    if (craftId == entry.Key)
                    {
                        break;
                    }

                    craftIndex++;
                }
                print("changeproductslot");
                invPotionDragDrop[craftIndex].SetActive(true);
                productSlot.GetComponent<Image>().sprite = invPotionDragDrop[craftIndex].GetComponent<IngredientDisplay>().ingredient.sprite;
                if (isOnMatTab)
                {
                    invPotionDragDrop[craftIndex].SetActive(false);
                }
                
            }
            print("cancraftfalse");
        }
        else
        {
            canCraft = false;
        }
    }

    public void CraftPotion()
    {
        if (canCraft)
        {
            if (invPotionDragDrop[craftIndex].GetComponent<IngredientDisplay>().ingredient.invAmount == 0)
            {
                invPotionDragDrop[craftIndex].GetComponent<InventoryDragDrop>().SetAlpha(1.0f);
            }
            ingredientManager.CombineIngredients(craftIngredient1.ingredientID, craftIngredient2.ingredientID);
            //create product drag drop
            LoadMaterials();
            LoadPotions();
            
            print("potion index " + craftIndex);
            //invPotionDragDrop[craftIndex].SetActive(true);
            //invPotionDragDrop[craftIndex].GetComponent<InventoryDragDrop>().rT.anchoredPosition = productSlot.rT.anchoredPosition;
            //invPotionDragDrop[craftIndex].GetComponent<InventoryDragDrop>().currSlot = productSlot;
            
            productSlot.GetComponent<Image>().sprite = productSlotDefault;

            craftIndex = -1;
            CraftSlotted(null, null, null);
            if(craftIngredient1.invAmount < 1)
            {
                craftDD1.SetAlpha(.7f);
            }
            if(craftIngredient2.invAmount < 1)
            {
                craftDD1.SetAlpha(.7f);
            }
        }
    }

    public void CraftUnslotted(InventoryDragDrop iS)
    {
        if (iS.currSlot == craftSlot1)
        {
            print("craft1 unslotted");
            craftIngredient1 = null;
            productSlot.GetComponent<Image>().sprite = productSlotDefault;
            craftIndex = -1;
            CraftSlotted(null, null, null);
        }
        else if (iS.currSlot == craftSlot2)
        {
            print("craft2 unslotted");
            craftIngredient2 = null;
            productSlot.GetComponent<Image>().sprite = productSlotDefault;
            craftIndex = -1;
            CraftSlotted(null, null, null);
        }
    }
}
