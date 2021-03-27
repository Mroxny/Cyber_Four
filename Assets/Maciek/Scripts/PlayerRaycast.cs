using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public GameObject originalTarget;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private bool gotowy = false;
    private GameObject prevTarget;

    void Start()
    {
        prevTarget = this.gameObject;
        originalTarget = this.gameObject;
        StartCoroutine("FindTargetsWithDelay", .2f);
    }
    IEnumerator FindTargetsWithDelay(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets() {
        originalTarget = this.gameObject;
        visibleTargets.Clear();
        Collider2D[] targetInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);

        for (int i = 0; i < targetInViewRadius.Length; i++) {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            Vector3 dstToOriginal = (originalTarget.transform.position - transform.position);
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2){
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) && (target.GetComponent<Interactable>() || target.GetComponentInParent<Interactable>())) {
                    if (originalTarget == this.gameObject) {
                        originalTarget = target.gameObject;
                    }
                    if (target.gameObject != originalTarget){
                        if (Vector2.Distance(transform.position, originalTarget.transform.position) > Vector2.Distance(transform.position, target.position))
                        {
                            originalTarget = target.gameObject;
                        }
                    }
                    visibleTargets.Add(target);
                }
            }
        }

        // sprawdza czy namierzony cel jest tez poprzednim
        if (originalTarget != prevTarget)
        {
            gotowy = true;
        }
        else if (originalTarget == prevTarget) {
            gotowy = false;
        }

        // sprawdza czy celem nie jest Player, oraz czy jest gotowy do zmiany
        if (originalTarget != this.gameObject && gotowy)
        {
            this.GetComponent<PlayerFOV>().inRange = true;
            this.GetComponent<PlayerFOV>().interakcja = originalTarget.GetComponent<Interactable>();
            if (originalTarget.GetComponent<Interactable>()) originalTarget.GetComponent<Interactable>().GetText(originalTarget.GetComponent<Interactable>().tekst);
            else if (originalTarget.GetComponentInParent<Interactable>()) originalTarget.GetComponentInParent<Interactable>().GetText(originalTarget.GetComponentInParent<Interactable>().tekst);
            if (prevTarget.GetComponent<Interactable>()) prevTarget.GetComponent<Interactable>().DestroyInteract();
            else if (prevTarget.GetComponentInParent<Interactable>()) prevTarget.GetComponentInParent<Interactable>().DestroyInteract();
        }

        //sprawdza czy celem jest gracz, zeby nie bylo bledow z znajdywaniem componentow itd.
        else if (originalTarget == this.gameObject && gotowy) {
            this.GetComponent<PlayerFOV>().inRange = false;
            this.GetComponent<PlayerFOV>().interakcja = null;
            if (prevTarget.GetComponent<Interactable>()) prevTarget.GetComponent<Interactable>().DestroyInteract();
            else if (prevTarget.GetComponentInParent<Interactable>()) prevTarget.GetComponentInParent<Interactable>().DestroyInteract();
        }

        prevTarget = originalTarget;
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
