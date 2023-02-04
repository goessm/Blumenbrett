using System;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<CardWithNumber> cards;
    private List<int> deck;
    // Start is called before the first frame update
    void Start()
    {
        ResetDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetDeck()
    {
        deck.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cards[i].number; j++)
            {
                deck.Add(i);
            }
        }
        Shuffle();
    }

    private void Shuffle()
    {

    }

    public List<GameObject> DrawCards(int num)
    {
        var min = Math.Min(num, deck.Count);
        var drawNumbers = deck.GetRange(deck.Count - 1 - min, min);
        deck = deck.GetRange(0, deck.Count - 1 - min);
        return drawNumbers.ConvertAll<GameObject>(ToCard);
    }

    public GameObject ToCard(int ind)
    {
        return cards[ind].card;
    }
}

[Serializable]
public class CardWithNumber
{
    public GameObject card;
    public int number;
}
