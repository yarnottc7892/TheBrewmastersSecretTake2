using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject inventoryUI;

    //Class to store data across scenes
    private SaveData localPlayerData = new SaveData();

    //Int to store health
    private int health = 30;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LoadPlayer();
        DontDestroyOnLoad(inventoryUI);
        //craftingMenu.SetActive(true);
        //invManager.LoadMaterials();
        //invManager.LoadPotions();
        //invManager.LoadMaterials();
        //craftingMenu.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SavePlayerOnEnemy();
            SceneManager.LoadScene(1);
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

        craftingMenu.SetActive(true);
        invManager.LoadMaterials();
        invManager.LoadPotions();
        invManager.LoadMaterials();
        craftingMenu.SetActive(false);

    }

}
