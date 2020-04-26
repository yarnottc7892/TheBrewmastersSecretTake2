using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class EnemyController : Combatant_Base, IDropHandler
{
    public Enemy_Base data;
    [SerializeField] private TextMeshProUGUI nameText;

    public int decidedAction;

    private void Start() {

        maxHealth = data.maxHealth;
        health = maxHealth;
        nameText.text = data.name;
        GetComponent<SpriteRenderer>().sprite = data.art;
        OnSetup?.Invoke(maxHealth);
    }
    public void OnDrop(PointerEventData eventData) 
    {

        CardController card = eventData.pointerDrag.GetComponent<CardController>();
        card.data.Play(transform, transform);
        card.discard();
    }

    private void OnMouseEnter() 
    {
        if (battle.currentCard != null)
        {
            if (!battle.currentCard.data.checkSelfTargeting())
            {
                battle.currentCard.targeting = true;
                transform.localScale = transform.localScale * 1.1f;
            }
        }
    }

    private void OnMouseExit() 
    {

        if (battle.currentCard != null)
        {
            battle.currentCard.targeting = false;
        }

        transform.localScale = Vector3.one * 0.5f;
    }

    public void decideTurn() 
    {
        decidedAction = Random.Range(0, 1000) % data.actions.Count;
        Debug.Log("Decided Action:" + decidedAction);
    }

    public IEnumerator takeTurn() 
    {
        if (health > 0)
        {
            startTurn();

            yield return new WaitForSeconds(0.5f);

            Transform target;
            if (data.actions[decidedAction].targetPlayer)
            {
                target = battle.enemy;
            } else
            {
                target = battle.player;
            }
            data.actions[decidedAction].DoEffect(target);

            yield return new WaitForSeconds(0.5f);

            battle.playerTurn();
        }
    }
}
