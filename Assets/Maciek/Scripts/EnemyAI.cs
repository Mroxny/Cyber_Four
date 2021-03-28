using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour {

    public bool staticSpeed = false;
    public float speed = 400f;
    public float nextWaypointDistance = 3f;
    public Animator animator;
    public EnemyType enemyType;
    public GameObject noticedIcon;
    public List<GameObject> weapons = new List<GameObject>();

    private GameObject player;
    private GameObject weapon;
    private GameObject weaponRender;
    private Vector2 target;
    private Vector2 startingPos;
    private State state = State.Roaming;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool canChangePos = true;
    Seeker seeker;
    Rigidbody2D rb;

    private enum State
    {
        Roaming,
        ChaseTarget,
    }

    public enum EnemyType
    {
        BigFish,
        TallGuy,
        Sneaky,
    }

    private void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
        weaponRender = gameObject.transform.Find("WeaponRender").gameObject;
        weapon = Instantiate(weapons[Random.Range(0, weapons.Count)],weaponRender.transform.position,Quaternion.identity);
        weapon.transform.parent = weaponRender.transform;
        InvokeRepeating("Updatepath", 0.1f, 0.2f);

        if (!staticSpeed) {

            switch (enemyType) {
                case EnemyType.BigFish:
                    speed = Random.Range(400,500);
                    break;
                case EnemyType.TallGuy:
                    speed = Random.Range(500, 650);
                    break;
                case EnemyType.Sneaky:
                    speed = Random.Range(650, 850);
                    break;
            }

        }
    }

    void Updatepath() {
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, target, OnPathComplete);
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate() {
        if (player == null) {
            player = GameObject.Find("Player(Clone)");
        }

        MoveTo();

        switch (state) {
            default:
            case State.Roaming:
                FindTarget();
                if (canChangePos) {
                    target = GetRoamingPos();
                    canChangePos = false;
                    StartCoroutine(WaitTime(Random.Range(3,7)));
                }
                break;
            case State.ChaseTarget:
                target = player.transform.position;
                break;
        } 
    }

    private IEnumerator WaitTime(int time) {
        yield return new WaitForSeconds(time);
        canChangePos = true;
    }
    private IEnumerator NoticePlayer(int time) {
        noticedIcon.SetActive(true);
        yield return new WaitForSeconds(time);
        noticedIcon.SetActive(false);
    }
    private void MoveTo() {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        }
        else {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        rb.AddForce(force);
        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }

    private Vector2 GetRoamingPos() {
        Vector2 randDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        return startingPos + randDir * Random.Range(5f,7f);
    }

    private void FindTarget() {
        if (Vector2.Distance(transform.position, player.transform.position) < Random.Range(9.5f,11f)) {
            //player in range
            state = State.ChaseTarget;
            StartCoroutine(NoticePlayer(3));
        }
    }

    public void TakeDamage(float dmg) {
    
    }
}
