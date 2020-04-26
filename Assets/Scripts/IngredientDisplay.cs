using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{
    public Ingredient ingredient;
    public Image image;
    public SpriteRenderer sr;
    public Sprite unknown;
    public bool isImage = true;
    bool firstRun = true;
    // Start is called before the first frame update
    void Start()
    {
        //SetImage();
        

        if (!isImage)
        {
            SetImage();
        }
        else if(firstRun && ingredient != null)
        {
            print("set to -1");
            ingredient.invAmount = -1;
            firstRun = false;
        }

    }

    public void SetImage()
    {
        if (isImage)
        {
            image = GetComponent<Image>();
            if (ingredient.invAmount < 0)
            {
                image.sprite = unknown;
            }
            else
            {
                image.sprite = ingredient.sprite;
            }
            if (ingredient.invAmount > -1)
            {
                GetComponentInChildren<Text>().text = "" + ingredient.invAmount;
            }
            else
            {
                GetComponentInChildren<Text>().text = "";
            }
        }
        else
        {
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = ingredient.sprite;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
