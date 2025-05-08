using UnityEngine;

public class FindObjectManager : MonoBehaviour
{
    private BoxCollider2D booxCollider;
    [SerializeField] private GameObject completedScreen;

    private void Awake()
    {
        booxCollider = GetComponent<BoxCollider2D>();

        if(booxCollider == null)
        {
            Debug.Log("No PolygonCollider2D found");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (booxCollider.OverlapPoint(mousePosition))
            {
                completedScreen.SetActive(true);
                Debug.Log("Completed");
            }
        }
    }
}