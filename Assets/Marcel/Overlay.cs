using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    public GameObject[] positions;
    public Deck deck;
    private bool isInSelection;

    // Start is called before the first frame update
    void Start()
    {
        ClearCards();
        isInSelection = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        Card.OnCardClicked += SelectCard;
    }


    void OnDisable()
    {
        Card.OnCardClicked -= SelectCard;
    }

    public void ClickDraw()
    {
        if (!isInSelection)
        {
            transform.position = new Vector3(0, -10, 0);
            isInSelection = true;
            PlaceCards(deck.DrawCards(3));
            transform.DOMoveY(0f,1).SetEase(Ease.OutQuad);
        }
    }

    public void ClearCards()
    {
        for(int i = 0; i < positions.Length; i++)
        {
            if(positions[i].transform.childCount > 0)
            {
                Destroy(positions[i].transform.GetChild(0).gameObject);
            }
        }
    }

    public void PlaceCards(List<GameObject> cards)
    {
        for (int i = 0; i < positions.Length && i < cards.Count; i++)
        {
            Debug.Log($"Instantiating {cards[i]} at {positions[i].transform}");
            Instantiate(cards[i], positions[i].transform);
        }
    }

    void SelectCard()
    {
        ClearCards();
        isInSelection = false;
    }
}
