using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private RectTransform discardPile;

    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;
    // Order in the sibling list
    private int sibOrder;
    private Animator anim;
    

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
        
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
 
    }

    // Card is clicked on
    public void OnPointerDown(PointerEventData eventData) 
    {
        // anim.SetBool("HoveredOver", false);
        sibOrder = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    // Card starts being dragged
    public void OnBeginDrag(PointerEventData eventData) 
    {
        foreach (Transform child in transform.parent.GetComponentInChildren<Transform>())
        {
            child.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        startPos = rectTransform.anchoredPosition;
    }

    // Card stops being dragged
    public void OnEndDrag(PointerEventData eventData) 
    {
        foreach (Transform child in transform.parent.GetComponentInChildren<Transform>())
        {
            child.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        transform.SetSiblingIndex(sibOrder);
    }

    // Card is being dragged
    public void OnDrag(PointerEventData eventData) 
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Card is hovered over
    public void OnPointerEnter(PointerEventData eventData) 
    {
        anim.SetBool("HoveredOver", true);
    }

    // Card not hovered over
    public void OnPointerExit(PointerEventData eventData) 
    {
        anim.SetBool("HoveredOver", false);
    }

    public IEnumerator discard() {

        Debug.Log("discarding");

        GetComponent<Animator>().SetTrigger("Discard");

        float journeyTime = 0.5f;
        float timePassed = 0f;

        Vector3 start = rectTransform.anchoredPosition;


        while(rectTransform.anchoredPosition != discardPile.anchoredPosition)
        {
            float fracJourney = timePassed / journeyTime;
            rectTransform.anchoredPosition = Vector3.Lerp(start, discardPile.anchoredPosition, fracJourney);

            timePassed += Time.deltaTime;

            yield return null;
        }

        if (rectTransform.anchoredPosition == discardPile.anchoredPosition)
        {
            Destroy(this.gameObject);
        }
    }
}
