using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    IngredientManager ingredientManager;
    public List<InventorySlot> invSlot;
    // Start is called before the first frame update
    void Start()
    {
        ingredientManager = GetComponent<IngredientManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
