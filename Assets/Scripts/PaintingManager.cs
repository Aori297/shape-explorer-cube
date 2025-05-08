using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]
public class PaintingManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject paintingToDisable;  // Assign current painting
    public GameObject paintingToEnable;   // Assign next painting

    private PolygonCollider2D polyCollider;
    [SerializeField] bool isLastClick;
    [SerializeField] TeacherDialoige td;

    private void Awake()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        if (polyCollider == null)
        {
            Debug.LogError("No PolygonCollider2D found on " + gameObject.name);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Convert screen point to world point
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(eventData.position);

        // Check if click is within the polygon collider
        if (polyCollider.OverlapPoint(clickPosition))
        {
            if (paintingToDisable != null) paintingToDisable.SetActive(false);
            if (paintingToEnable != null) paintingToEnable.SetActive(true);
        }

        if(isLastClick)
        {
            Debug.Log("Last click detected");
            td.nextDialogue();
        }
    }
}