using UnityEngine;

public class Card : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnCardClicked;

    public GameObject rootPrefab;
    public GameObject highlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        highlight.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
            OnCardClicked();
        }
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
