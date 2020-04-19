using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IDragHandler
{
    
    
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI description;

    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;
    

    public Card_Base data;
    public Vector3 startPos;
    public RectTransform rectTransform;


    // Start is called before the first frame update
    void Start()
    {
        cost.text = data.cost.ToString();
        description.text = data.description.ToString();

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startPos = rectTransform.anchoredPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            data.Play();
        }
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        // Maybe do something, maybe do nothing
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        // data.Play();
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData) 
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
