using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour
{

    public string fullText;
    private string currentText = "";
    private int counter = 0;
    private Text text;
    private float delay = 0.0f;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentText.Length != fullText.Length)
        {
            if (delay <= 0.0f)
            {
                currentText = fullText.Substring(0, counter);
                text.text = currentText;
                counter++;
                delay = 0.1f;
            }
            else
            {
                delay -= Time.deltaTime;
            }
        }
        else
        {
            done = true;
        }


        if (Input.GetKeyDown(KeyCode.E) && done && GameManager.textBoxOn)
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
            GameManager.textBoxOn = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !done && GameManager.textBoxOn)
        {
            currentText = fullText;
            text.text = currentText;
        }
    }
}
