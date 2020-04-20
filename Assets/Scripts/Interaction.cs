using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private bool overlap = false;
    public GameObject textBox;
    public string textString;
    private Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && overlap && !GameManager.textBoxOn)
        {
            var textB = Instantiate(textBox);
            textB.transform.SetParent(canvas.transform);
            textB.transform.GetChild(0).GetComponent<ScrollText>().fullText = textString;
            GameManager.textBoxOn = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            overlap = true;
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
