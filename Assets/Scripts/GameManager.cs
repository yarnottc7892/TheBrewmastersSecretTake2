using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveData
{
    public Vector2 playerPosition = Vector2.zero;
    public int health = 30;
    public Enemy_Base enemy = null;
    public bool dropItem = false;
    public Vector2 enemyPos = Vector2.zero;
}

public class GameManager : MonoBehaviour
{
    public static bool textBoxOn = false;

    public static GameManager Instance { get; private set; }

    public GameObject canvasPrefab;

    public GameObject canvas;

    public SaveData savedPlayerData;

    public GameObject textBox;

    public GameObject craftingUI;

    public List<Card_Base> cards = new List<Card_Base>();

    public List<Card_Base> deck = new List<Card_Base>();

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            savedPlayerData = new SaveData();
            Instance.canvas = Instantiate(Instance.canvasPrefab);
            Instance.craftingUI = canvas.transform.GetChild(0).gameObject;
            Instance.textBox = canvas.transform.GetChild(1).gameObject;

            textBox.SetActive(false);

            canvas.transform.GetChild(0).gameObject.GetComponent<IngredientManager>().ResetAll();

            DontDestroyOnLoad(canvas);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddCard(string id)
    {
        var card = Instance.cards.Find(x => x.name == id);
        Instance.deck.Add(card);
    }

    public static void RemoveCard(string id)
    {
        var card = Instance.cards.Find(x => x.name == id);
        Instance.deck.Remove(card);
    }

}


