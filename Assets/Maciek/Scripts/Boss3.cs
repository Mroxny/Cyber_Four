using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss3 : MonoBehaviour
{
    public float Life;
    public float speed = 400f;
    public Animator animator;
    public GameObject healthbar;

    [HideInInspector]
    public float nextWaypointDistance = 3f;

    private GameObject player;
    private Bulid_Script bs;
    private float maxLife;
    private Vector2 target;
    private Vector2 startingPos;
    private Direction dir = Direction.Right;
    private AudioManager am;

    private bool isOn = false;
    private bool canDie = false;
    private bool canHurt = true;
    private bool oneTime = true;

    private bool reachedEndOfPath = false;
    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Rigidbody2D rb;

    private enum Direction
    {
        Left,
        Right
    }


    private void Start()
    {

        GameObject sceneMenager = GameObject.FindWithTag("SceneMenager");
        bs = sceneMenager.GetComponent<Bulid_Script>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
        target = startingPos;
        maxLife = Life;
        InvokeRepeating("UpdatePath", 0.1f, 0.3f);


    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (player == null) player = GameObject.Find("Player(Clone)");
        if (oneTime)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 7f)
            {
                TurnOn();
            }
        }

        if (isOn) { 

                

                target = player.transform.position - new Vector3(0, 2, 0);

                if (Vector2.Distance(transform.position, player.transform.position) <= 3f)
                {
                    switch (Random.Range(1, 6))
                    {
                        case 1:
                            animator.SetTrigger("Attack_1");
                            break;
                        case 2:
                            animator.SetTrigger("Attack_2");
                            break;
                        case 3:
                            animator.SetTrigger("Attack_3");
                            break;
                        case 4:
                            animator.SetTrigger("Attack_4");
                            break;
                        case 5:
                            animator.SetTrigger("Attack_5");
                            break;
                    }
                }


                MoveTo();


            
        }

    }


    public void TurnOn()
    {
        animator.SetTrigger("TurnOn");
        am.Play("boss_1_sound");
        StartCoroutine(WakeUp(2f));
        oneTime = false;
    }


    public void takeDamage(float damage)
    {
        if (canDie)
        {
            Life -= damage;
            healthbar.transform.localScale = new Vector2(Mathf.Clamp(ExtensionMethods.Remap(Life, 0, maxLife, 0, 3), 0, 3), 45);
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("boss_1_hit");
            if (Life <= 0)
            {
                StartCoroutine(die());
                canDie = false;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (canHurt && collision.tag == "Player")
        {
            player.GetComponent<Player>().DamagePlayer();
        }

        if (collision.tag == "Bullet" && collision.GetComponent<Bullet>().friendly == true)
        {
            takeDamage(collision.GetComponent<Bullet>().damage);

            GameObject.Destroy(collision.gameObject);

        }



    }




    public void StartHurting()
    {
        canHurt = true;
    }

    public void StopHurting()
    {
        canHurt = false;
    }

    private Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }

    

    IEnumerator die()
    {
        isOn = false;
        healthbar.SetActive(false);
        animator.SetTrigger("Die");
        transform.Find("MinimapIcon").gameObject.SetActive(false);
        speed = 0;

        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("boss_1_sound");

        yield return new WaitForSeconds(.5f);
        GameObject.Find("SceneMenager").GetComponent<Bulid_Script>().BossDied();

        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    

    IEnumerator WakeUp(float time)
    {
        yield return new WaitForSeconds(time);

        isOn = true;

        canDie = true;
        string text = "Defeat Opponent";
        PlayerPrefs.SetString("CurrentTask", text);
        player.GetComponent<Player>().Notify(text, 3);

    }



    private void MoveTo()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;


        if (rb.velocity.x > 0 && dir != Direction.Right)
        {
            dir = Direction.Right;
            transform.localScale *= new Vector2(-1, 1);
        }
        else if (rb.velocity.x < 0 && dir != Direction.Left)
        {
            dir = Direction.Left;
            transform.localScale *= new Vector2(-1, 1);
        }

        rb.AddForce(force);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}