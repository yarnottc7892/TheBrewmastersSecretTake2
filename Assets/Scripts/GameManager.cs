using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveData
{
    public Vector2 playerPosition = Vector2.zero;
    public int health = 30;
    public Enemy_Base enemy = null;
}

public class GameManager : MonoBehaviour
{
    public static bool textBoxOn = false;

    public static GameManager Instance { get; private set; }

    public GameObject canvasPrefab;

    public GameObject canvas;

    public SaveData savedPlayerData;

    public GameObject craftingUIPrefab;

    public GameObject textBoxUIPrefab;

    public GameObject textBox;

    public GameObject craftingUI;

    public List<Card_Base> deck = new List<Card_Base>();

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            savedPlayerData = new SaveData();
            Instance.canvas = Instantiate(Instance.canvasPrefab);
            Instance.craftingUI = Instantiate(Instance.craftingUIPrefab);
            Instance.textBox = Instantiate(Instance.textBoxUIPrefab);

            Instance.craftingUI.transform.SetParent(Instance.canvas.transform);
            Instance.textBox.transform.SetParent(Instance.canvas.transform);
            textBox.SetActive(false);

            DontDestroyOnLoad(canvas);
            //DontDestroyOnLoad(craftingUI);
            //DontDestroyOnLoad(textBox);
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

    }

    private static Card_Base CreateCard()
    {
        return new Card_Base();
    }
}


