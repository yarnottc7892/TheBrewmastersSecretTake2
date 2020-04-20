using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{
    public Ingredient ingredient;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = ingredient.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
