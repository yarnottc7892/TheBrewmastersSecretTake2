using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private bool overlap = false;
    public GameObject textBox;
    public string textString;
    private GameObject player;
    public Ingredient ingredient;

    // Start is called before the first frame update
    void Start()
    {
        textString = "Obtained " + ingredient.ingredientDisplayName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && overlap && !GameManager.textBoxOn)
        {
            textBox.SetActive(true);
            textBox.transform.GetChild(0).gameObject.SetActive(true);
            textBox.transform.GetChild(0).GetComponent<ScrollText>().fullText = textString;
            var playerCont = player.GetComponent<PlayerControl>();

            player.GetComponent<PlayerControl>().ingManager.AddIngredient(ingredient.ingredientID);
            GameManager.textBoxOn = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            overlap = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            overlap = false;
        }
    }
}
