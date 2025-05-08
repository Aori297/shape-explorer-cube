using UnityEngine;

public class FindObjectManager : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;
    [SerializeField] private GameObject completedScreen;

    private void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();

        if(polygonCollider == null)
        {
            Debug.Log("No PolygonCollider2D found");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (polygonCollider.OverlapPoint(mousePosition))
            {
                completedScreen.SetActive(true);
                Debug.Log("Completed");
            }
        }
    }
}