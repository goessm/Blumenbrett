using UnityEngine;

public class Card : MonoBehaviour
{
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
        }
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
