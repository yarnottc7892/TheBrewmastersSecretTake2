using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{
    public Ingredient ingredient;
    public Image image;
    public Sprite unknown;
    // Start is called before the first frame update
    void Start()
    {
        //SetImage();
        if(ingredient != null)
        {
            ingredient.invAmount = -1;
        }
        
    }

    public void SetImage()
    {
        image = GetComponent<Image>();
        if(ingredient.invAmount < 0)
        {
            image.sprite = unknown;
        }
        else
        {
            image.sprite = ingredient.sprite;
        }
        if(ingredient.invAmount > -1)
        {
            GetComponentInChildren<Text>().text = "" + ingredient.invAmount;
        }
        else
        {
            GetComponentInChildren<Text>().text = "";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
