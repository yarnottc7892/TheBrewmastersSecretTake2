using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    // Card elements to be set
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI description;

    // Places the card needs to go
    [SerializeField] private RectTransform discardPile;
    [SerializeField] private RectTransform playSpot;
    

    // Utilities
    [SerializeField] private BattleManager battle;
    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Animator anim;
    private bool selected = false;
    

    // Order in the sibling list
    private int sibOrder;

    // Things that need to be accessed by other classes
    public Card_Base data;
    public Vector3 startPos;
    public RectTransform rectTransform;
    public bool targeting = false;
    public bool playSelfTargetedCard = false;

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

    // Check what to do when the mouse button is lifted
    public void OnPointerUp(PointerEventData eventData) 
    {
        selected = false;
        anim.SetBool("HoveredOver", false);
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
            if (playSelfTargetedCard)
            {
                data.Play(battle.player, battle.enemy);
                StartCoroutine(discard());
            }
            else
            {
                rectTransform.anchoredPosition = startPos;
            }
                
        }

        battle.currentCard = null;
        
    }
    // Card is clicked on
    public void OnPointerDown(PointerEventData eventData) 
    {
        sibOrder = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        rectTransform.anchoredPosition = playSpot.anchoredPosition;
        battle.currentCard = this;
        selected = true;
    }

    // Card is hovered over
    public void OnPointerEnter(PointerEventData eventData) 
    {
        anim.SetBool("HoveredOver", true);
    }

    // Card not hovered over
    public void OnPointerExit(PointerEventData eventData) 
    {
        if (!selected)
        {
            anim.SetBool("HoveredOver", false);
        }
    }

    public IEnumerator discard() {

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
