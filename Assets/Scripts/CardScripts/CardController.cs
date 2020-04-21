using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    
    
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private RectTransform discardPile;
    [SerializeField] private RectTransform playSpot;
    [SerializeField] private BattleManager battle;

    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;
    // Order in the sibling list
    private int sibOrder;
    private Animator anim;
    

    public Card_Base data;
    public Vector3 startPos;
    public RectTransform rectTransform;
    public bool targeting = false;


    // Start is called before the first frame update
    void Start()
    {
        cost.text = data.cost.ToString();
        description.text = data.setDescription();

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        if (!data.checkSelfTargeting())
        {
            if (!targeting)
            {
                rectTransform.anchoredPosition = startPos;

            } 
            else
            {
                data.Play(battle.player, battle.enemy);
                StartCoroutine(discard());
            }
        }
        else
        {
            data.Play(battle.player, battle.enemy);
            StartCoroutine(discard());
        }

        battle.currentCard = null;
        
    }
    // Card is clicked on
    public void OnPointerDown(PointerEventData eventData) 
    {
        // anim.SetBool("HoveredOver", false);
        sibOrder = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        rectTransform.anchoredPosition = playSpot.anchoredPosition;
        battle.currentCard = this;
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
