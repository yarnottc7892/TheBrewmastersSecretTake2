using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour, IDropHandler
{
    [SerializeField] BattleManager battle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData) {

        CardController card = eventData.pointerDrag.GetComponent<CardController>();
        card.data.Play(transform, transform);
        StartCoroutine(card.discard());
    }

    private void OnMouseEnter() {
        if (battle.currentCard != null)
        {
            battle.currentCard.targeting = true;
        }

        transform.localScale = transform.localScale * 1.2f;
    }

    private void OnMouseExit() {
        if (battle.currentCard != null)
        {
            battle.currentCard.targeting = false;
        }

        transform.localScale = Vector3.one * 0.6741f;
    }
}
