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
        Shuffle(deck);
    }

    public void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public List<GameObject> DrawCards(int num)
    {
        var min = Math.Min(num, deck.Count);
        var drawNumbers = deck.GetRange(deck.Count - 1 - min, min);
        deck = deck.GetRange(0, deck.Count - 1 - min);
        if (deck.Count == 0)
        {
            ResetDeck();
        }
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
