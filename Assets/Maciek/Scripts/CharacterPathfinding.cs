using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathfinding : MonoBehaviour
{

    private const float speed = 5f;

    private int currentPathIndex;
    private List<Vector2> pathVectorList;


    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector2 targetPos = pathVectorList[currentPathIndex];
            if (Vector2.Distance(transform.position, targetPos) > 1f) {
                Vector2 moveDir = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;

                float distanceBefore = Vector2.Distance(transform.position, targetPos);
                transform.position = transform.position + new Vector3(moveDir.x, moveDir.y, 0) * speed * Time.deltaTime;
            }
            else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            }
        }
        else {
        
        }
    }

    private void StopMoving() {
        pathVectorList = null;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    public void MoveTo(Vector2 targetPos) {
        SetTargetPos(targetPos);
        
    }
    public void SetTargetPos(Vector2 targetPos) {
        currentPathIndex = 0;
        pathVectorList = PathFinding.Instance.FindPath(GetPosition(), targetPos);

        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }
}
