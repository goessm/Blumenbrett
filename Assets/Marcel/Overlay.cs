using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    public GameObject[] positions;

    // Start is called before the first frame update
    void Start()
    {
        ClearCards();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Instantiate(cards[i], positions[i].transform);
        }
    }
}
