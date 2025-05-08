using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class DragArrow : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float maxDragDistance = 10f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float directionDotThreshold = 0f;
    [SerializeField] float minDistanceToAddPoint = 0.1f;
    [SerializeField] bool hasBranch = false;
    [SerializeField] String branchText;
    [SerializeField] TextMeshProUGUI noBranchText;
    private int currentWaypointIndex = 0;
    private bool isDragging = false;
    public LineRenderer lineRenderer;
    private Vector3 lastLinePoint;
    private SpriteRenderer spriteRenderer;
    BranchManager bm;

    void Start()
    {
        //  lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, transform.position);
            lastLinePoint = transform.position;
        }
        if (hasBranch)
        {
            bm = BranchManager.instance;
            bm.branchText.text = branchText;
        }
        else
        {
            noBranchText.text = branchText;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

            if (hit != null && hit.transform == transform)
            {
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && currentWaypointIndex < waypoints.Length)
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            float distanceToCursor = Vector3.Distance(transform.position, mouseWorldPos);
            if (distanceToCursor > maxDragDistance) return;

            Vector3 dragDirection = (mouseWorldPos - transform.position).normalized;
            Vector3 pathDirection = (waypoints[currentWaypointIndex].position - transform.position).normalized;

            if (Vector3.Dot(dragDirection, pathDirection) > directionDotThreshold)
            {
                FaceNextWaypoint();

                Vector3 target = waypoints[currentWaypointIndex].position;
                Vector3 oldPosition = transform.position;

                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);

                // Check if moved enough to add a new point
                if (Vector3.Distance(transform.position, lastLinePoint) >= minDistanceToAddPoint)
                {
                    AddLinePoint(transform.position);
                }

                if (Vector3.Distance(transform.position, target) < 0.1f)
                {
                    currentWaypointIndex++;
                    if (currentWaypointIndex == waypoints.Length)
                    { 
                        spriteRenderer.enabled = false;
                        if (!hasBranch)
                        {
                            Debug.Log("Complete");
                            SceneManager.LoadScene("Cube");
                        }
                        else
                        {
                            bm.NextBranch();

                            this.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    void AddLinePoint(Vector3 newPoint)
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPoint);
        lastLinePoint = newPoint;
    }

    private void FaceNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 dir = waypoints[currentWaypointIndex].position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
