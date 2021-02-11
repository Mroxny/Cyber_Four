using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    public float viewRadius;
    [Range (0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;


    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    IEnumerator FindTargetsWithDelay(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets() {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 DirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.position);
            if (!Physics.Raycast(transform.position, DirToTarget, dstToTarget, obstacleMask)) {
                visibleTargets.Add(target);
            }
        }

    }
    public Vector3 DirFromAngle(float AngleInDeg, bool angleIsGlobal) {

        if (!angleIsGlobal) {
            AngleInDeg += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(AngleInDeg * Mathf.Deg2Rad), Mathf.Cos(AngleInDeg * Mathf.Deg2Rad), 0);
    }
}
