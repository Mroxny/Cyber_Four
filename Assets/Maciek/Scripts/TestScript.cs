using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private CharacterPathfinding charPath;
    private Grid<PathNode> grid;
    private PathFinding pathFinding;

    void Start()
    {
        pathFinding = new PathFinding(5, 5);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mouseWorldPos = GetMouseWorldPosition();
            pathFinding.GetGrid().GetXY(mouseWorldPos, out int x, out int y);
            List<PathNode> path = pathFinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i = 0; i < path.Count - 1; i++) {
                    Debug.DrawLine(new Vector2(path[i].x, path[i].y)* 10f + Vector2.one *5f, new Vector2(path[i+1].x, path[i+1].y) * 10f + Vector2.one * 5f, Color.white, 100f);
                }
            }
            charPath.SetTargetPos(mouseWorldPos);
        }
    }









    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ() {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

}
