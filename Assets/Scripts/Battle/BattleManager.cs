﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class BattleManager : MonoBehaviour
{

    public enum BattleState { Start, EnemyTurn, PlayerTurn, Win, Lose }
    private BattleState state = BattleState.Start;
    private int round = 0;
    public int energyPerTurn;
    public int currentEnergy;

    [Header("References Needed")]
    [SerializeField] private RectTransform drawDeck;
    [SerializeField] private RectTransform discardDeck;
    [SerializeField] private CardPool pool;
    [SerializeField] private Deck deck;
    [SerializeField] private TextMeshProUGUI cardsInDraw;
    [SerializeField] private TextMeshProUGUI cardsInDiscard;
    [SerializeField] private RectTransform middleCardSpot;
    [SerializeField] private TextMeshProUGUI energy;

    [Header("Hand And Card Settings")]
    [SerializeField] private int handSize = 5;
    [SerializeField] private float handStartingHeight;
    [SerializeField] private float maxHandYDeviation;
    [SerializeField] private float maxHandRotDeviation;
    [SerializeField] private float distanceBetweenCards = 200f;
    private List<CardController> cardsInHand = new List<CardController>();

    [Header("Referenced By Other Classes")]
    public Transform player;
    public Transform enemy;

    public CardController currentCard = null;

    // Start is called before the first frame update
    void Start() 
    {
        if (state == BattleState.Start)
        {
            deck.shuffleDraw();
            state = BattleState.PlayerTurn;

            playerTurn();
        }
    }

    private void drawCard() 
    {
        if (deck.discard.Count == 0 && deck.draw.Count == 0)
        {
            Debug.Log("Can't draw anymore cards");
        }
        else
        {
            var card = CardPool.Instance.Get();

            if (deck.draw.Count == 0)
            {
                deck.shuffleDiscard();
                cardsInDraw.text = deck.draw.Count.ToString();
                cardsInDiscard.text = 0.ToString();
            }

            card.setData(deck.draw[deck.draw.Count - 1]);
            deck.removeCardFromDrawAt(deck.draw.Count - 1);
            cardsInDraw.text = deck.draw.Count.ToString();
            card.gameObject.SetActive(true);

            cardsInHand.Add(card);

            RectTransform cardRect = card.GetComponent<RectTransform>();
            cardRect.anchoredPosition = drawDeck.anchoredPosition;
            cardRect.localRotation = new Quaternion(0f, 0f, 100f, cardRect.rotation.w);
            cardRect.DOScale(Vector3.one, 0.5f);

            generateCardPositions();
        }
    }

    private void drawHand() 
    {
        for (int i = 0; i < handSize; i++)
        {
            drawCard();
        }
    }

    private void generateCardPositions() {

        float distanceFromMiddle = 0f;
        float firstCardXPos = 0f;

        if (cardsInHand.Count % 2 == 0)
        {
            distanceFromMiddle = distanceBetweenCards / 2;
            firstCardXPos = middleCardSpot.anchoredPosition.x + distanceFromMiddle + (distanceBetweenCards * ((cardsInHand.Count / 2) - 1));
        } 
        else
        {
            firstCardXPos = middleCardSpot.anchoredPosition.x + distanceFromMiddle + (distanceBetweenCards * (cardsInHand.Count / 2));
        }

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            RectTransform cardRect = cardsInHand[i].GetComponent<RectTransform>();

            float newXPos = firstCardXPos - (distanceBetweenCards * i);

            float newYPos = handStartingHeight;
            float newRotation = 0f;


            if (cardsInHand.Count % 2 == 0)
            {

            }
            if ((cardsInHand.Count / 2) > 0)
            {
                float distanceFromCenterWeight = (float)((cardsInHand.Count / 2) - i) / (cardsInHand.Count / 2);
                newYPos -= (Mathf.Abs(distanceFromCenterWeight) * maxHandYDeviation);
                newRotation = (-1 * distanceFromCenterWeight) * maxHandRotDeviation;
            }

            Vector2 newPos = new Vector2(newXPos, newYPos);
            Vector3 newRot = new Vector3(0f, 0f, newRotation);

            cardRect.DOAnchorPos(newPos, 0.5f).OnComplete(setCardPositions);
            cardRect.DORotate(newRot, 0.5f);
            cardsInHand[i].startRot = newRot;

        }
    }

    private void setCardPositions() 
    {
        foreach(CardController card in cardsInHand)
        {
            card.startPos = card.GetComponent<RectTransform>().anchoredPosition;
        }
        
    }

    public void removeCard(CardController card) 
    {
        if (state == BattleState.PlayerTurn)
        {
            currentEnergy -= card.data.cost;
            energy.text = currentEnergy + "/" + energyPerTurn;
        }
        cardsInHand.Remove(card);
        deck.addCardToDiscard(card.data);
        cardsInDiscard.text = deck.discard.Count.ToString();
        generateCardPositions();
    }

    public void playerTurn() 
    {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        EnemyController enemyScript = enemy.GetComponent<EnemyController>();

        if(playerScript.health <= 0)
        {
            playerWin();
            return;
        }
        else if(enemyScript.health <= 0)
        {
            playerLose();
            return;
        }
        enemyScript.GetComponent<EnemyController>().decideTurn();


        playerScript.startTurn();

        drawHand();
        cardsInDraw.text = deck.draw.Count.ToString();
        cardsInDiscard.text = 0.ToString();

        currentEnergy = energyPerTurn;
        energy.text = currentEnergy + "/" + energyPerTurn;


        foreach (CardController card in cardsInHand)
        {
            card.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        state = BattleState.PlayerTurn;
    }

    private void enemyTurn() 
    {

        EnemyController enemyScript = enemy.GetComponent<EnemyController>();

        foreach(CardController card in cardsInHand)
        {
            card.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        round++;

        StartCoroutine(enemyScript.takeTurn());
    }

    public void endTurn() 
    {
        state = BattleState.EnemyTurn;

        for (int i = cardsInHand.Count - 1; i >= 0; i--)
        {
            cardsInHand[i].discard();
        }

        enemyTurn();
    }

    private void playerWin() 
    {
        Debug.Log("Player Wins!");
    }

    private void playerLose() {
        Debug.Log("Player Loses :(");
    }
}
