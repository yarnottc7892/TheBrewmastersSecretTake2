using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveData
{
    public Vector2 playerPosition = Vector2.zero;
    public int health = 30;
    public List<Card_Base> deck = new List<Card_Base>();
    public Enemy_Base enemy = null;
}

public class GameManager : MonoBehaviour
{
    public static bool textBoxOn = false;


    public static GameManager Instance { get; private set; }

    public SaveData savedPlayerData;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            savedPlayerData = new SaveData();
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
}


