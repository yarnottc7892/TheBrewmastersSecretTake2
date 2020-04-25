using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Float used to adjust player speed
    [SerializeField] private float speed;

    //Reference to the ridgidbody2D
    private Rigidbody2D rb;

    //Refernce to the crafting menu
    public GameObject craftingMenu;

    //Reference to the ingredient Manager
    public IngredientManager ingManager;

    //Reference to the inventory Manager
    public InventoryManager invManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        craftingMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!craftingMenu.activeInHierarchy)
        {
            float moveHori = Input.GetAxis("Horizontal");
            float moveVert = Input.GetAxis("Vertical");

            if (!GameManager.textBoxOn)
            {
                rb.velocity = new Vector2(moveHori, moveVert) * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            craftingMenu.SetActive(!craftingMenu.activeInHierarchy);
            invManager.LoadMaterials();
        }
    }
}
