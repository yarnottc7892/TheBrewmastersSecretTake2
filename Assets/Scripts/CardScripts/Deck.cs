using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card_Base> draw = new List<Card_Base>();
    public List<Card_Base> discard = new List<Card_Base>();

    public Card_Base getCardAt(int index) 
    {
        return draw[index];
    }

    public void shuffleDraw() 
    {

        int cardsToShuffle = draw.Count;

        System.Random rng = new System.Random();
        while (cardsToShuffle > 0)
        {
            cardsToShuffle--;
            int differentCard = rng.Next(cardsToShuffle);
            Card_Base temp = draw[differentCard];
            draw[differentCard] = draw[cardsToShuffle];
            draw[cardsToShuffle] = temp;
        }
    }

    public void shuffleDiscard() 
    {

        foreach(Card_Base card in discard)
        {
            draw.Add(card);
        }

        shuffleDraw();

        discard.Clear();
    }

    public void addCardToDiscard(Card_Base discardedCard) 
    {
        discard.Add(discardedCard);
    }

    public void removeCardFromDrawAt(int drawnCardIndex) 
    {
        draw.RemoveAt(drawnCardIndex);
    }
}
