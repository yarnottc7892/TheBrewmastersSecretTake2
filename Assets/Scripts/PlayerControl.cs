using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //Float used to adjust player speed
    [SerializeField] private float speed;

    private Animator anim;

    //Reference to the ridgidbody2D
    private Rigidbody2D rb;

    //Reference to the crafting menu
    public GameObject craftingMenu;

    //Reference to the ingredient Manager
    public IngredientManager ingManager;

    //Reference to the inventory Manager
    public InventoryManager invManager;

    //Class to store data across scenes
    private SaveData localPlayerData = new SaveData();

    //Int to store health
    private int health = 30;

    bool firstRun = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LoadPlayer();
        //craftingMenu.SetActive(true);

        anim = GetComponent<Animator>();
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (firstRun)
        {
            craftingMenu.SetActive(true);
            invManager.LoadMaterials();
            invManager.LoadPotions();
            invManager.LoadMaterials();
            craftingMenu.SetActive(false);
            firstRun = false;
        }

        if (!craftingMenu.activeInHierarchy)
        {
            float moveHori = Input.GetAxis("Horizontal");
            float moveVert = Input.GetAxis("Vertical");

            if (moveHori > 0)
            {
                anim.SetTrigger("WalkRight");
            }
            else if (moveHori < 0)
            {
                anim.SetTrigger("WalkLeft");
            }
            else if (moveVert > 0)
            {
                anim.SetTrigger("WalkBackwards");
            }
            else if(moveVert < 0)
            {
                anim.SetTrigger("WalkForwards");
            } else
            {
                anim.SetTrigger("IdleForward");
            }

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
            if (!craftingMenu.activeInHierarchy)
            {
                invManager.SetCraftIngredientsBackToInvSlot();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SavePlayerOnEnemy();
            SceneManager.LoadScene("BattleScene");
        }
    }

    //Used to save data on an enemy encounter
    private void SavePlayerOnEnemy()
    {
        localPlayerData.playerPosition = transform.position;
        //localPlayerData.enemy = enemy;
        localPlayerData.health = health;

        GameManager.Instance.savedPlayerData = localPlayerData;
    }

    private void LoadPlayer()
    {
        localPlayerData = GameManager.Instance.savedPlayerData;


        transform.position = localPlayerData.playerPosition;
        health = localPlayerData.health;

        GameObject temp = GameManager.Instance.canvas.transform.GetChild(0).gameObject;
        craftingMenu = temp.transform.GetChild(0).gameObject;
        invManager = temp.GetComponent<InventoryManager>();
        ingManager = temp.GetComponent<IngredientManager>();

        craftingMenu.SetActive(true);
        invManager.LoadMaterials();
        craftingMenu.SetActive(false);

    }

}
