using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss3 : MonoBehaviour
{
    public float Life;
    public float speed = 500f;
    public float nextWaypointDistance = 3f;
    public Animator animator;
    public GameObject healthbar;
    public GameObject[] weaponsToLeave;



    private GameObject player;
    private float maxLife;
    private Vector2 target;
    private Direction dir = Direction.Right;
    private AudioManager am;
    private float damageMultiplier = 1;

    private bool canDie = false;
    private bool canHurt = true;
    private bool canShoot = true;
    private bool oneTime = true;
    private bool isOn = false;

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
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = transform.position;
        maxLife = Life;
        InvokeRepeating("UpdatePath", 0.1f, 0.11f);


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

                if (Vector2.Distance(transform.position + new Vector3(0, 2, 0), player.transform.position) <= 3.5f)
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
        StartCoroutine(WakeUp(2f));
        StartHurting();
        oneTime = false;
        if (am != null) am.Play("boss_1_sound");

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
            takeDamage(collision.GetComponent<Bullet>().damage * damageMultiplier);

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

    public void EnableMultiplier()
    {
        damageMultiplier = Random.Range(.3f, .5f);
    }

    public void DisableMultiplier()
    {
        damageMultiplier = 1;
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

        Instantiate(weaponsToLeave[Random.Range(0, weaponsToLeave.Length)], transform.position, Quaternion.identity);


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
        StopHurting();

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