
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(PlayerRaycast))]
public class PlayerRaycastEditor : Editor
{
    void OnSceneGUI()
    {
        PlayerRaycast pr = (PlayerRaycast)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(pr.transform.position, Vector3.forward, Vector3.up, 360, pr.viewRadius);
        Vector3 viewAngleA = pr.DirFromAngle(-pr.viewAngle / 2, false);
        Vector3 viewAngleB = pr.DirFromAngle(pr.viewAngle / 2, false);

        Handles.DrawLine(pr.transform.position, pr.transform.position + viewAngleA * pr.viewRadius);
        Handles.DrawLine(pr.transform.position, pr.transform.position + viewAngleB * pr.viewRadius);

        foreach (Transform visibleTarget in pr.visibleTargets) {
            Handles.color = Color.red;
            Handles.DrawLine(pr.transform.position, visibleTarget.position);
        }
    }
}*/

