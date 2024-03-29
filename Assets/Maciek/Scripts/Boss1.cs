﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss1 : MonoBehaviour
{
    public float Life;
    public float speed = 400f;
    public float nextWaypointDistance = 3f;
    public Animator animator;
    public GameObject healthbar;
    public GameObject[] weaponsToLeave;

    private GameObject player;
    private Bulid_Script bs;
    private Vector2 target;
    private Vector2 startingPos;
    private Mode mode = Mode.Rest;
    private bool canDie = false;
    private bool canHurt = true;
    private float maxLife;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool oneTime = true;
    bool canChange = true;
    Seeker seeker;
    Rigidbody2D rb;

    private enum Mode
    {
        Rest,
        ChaseTarget,
        GrivMode,
    }

    private void Start() {
        
        GameObject sceneMenager = GameObject.FindWithTag("SceneMenager");
        bs = sceneMenager.GetComponent<Bulid_Script>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
        target = startingPos;
        maxLife = Life;
        InvokeRepeating("UpdatePath", 0.1f, 0.3f);
        

    }

    void UpdatePath() {
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

    private void FixedUpdate() {
        if (player == null) player = GameObject.Find("Player(Clone)");
        if (oneTime) {
            if (Vector2.Distance(transform.position, player.transform.position) < 7f) {
                TurnOn();
            }
        }

        switch (mode) {
            case Mode.ChaseTarget:

                if (canChange) {
                    StartCoroutine(ChangeModeAfter(15, Mode.GrivMode));
                    StopHurting();
                    speed = 650;
                    animator.SetBool("GrivMode", false);
                    canChange = false;
                }

                target = LerpByDistance(transform.position, player.transform.position, 10f);

                if (Vector2.Distance(transform.position, player.transform.position) <= 3.5f) {
                    switch (Random.Range(1, 4)) {
                        case 1:
                            animator.SetTrigger("Attack_1");
                            break;
                        case 2:
                            animator.SetTrigger("Attack_2");
                            break;
                        case 3:
                            animator.SetTrigger("Attack_3");
                            break;
                    }
                }
                MoveTo();

                break;

            case Mode.GrivMode:

                if (canChange) {
                    StartCoroutine(ChangeModeAfter(20, Mode.ChaseTarget));
                    StartHurting();
                    speed = 400;
                    animator.SetBool("GrivMode", true);
                    canChange = false;
                }
                target = player.transform.position;

                MoveTo();
                break;
        }

    }


    public void TurnOn() {
        GetComponent<Animator>().SetTrigger("TurnOn");
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("boss_1_sound");
        StartCoroutine(WakeUp(1.5f));
        oneTime = false;
    }


    public void takeDamage(float damage) {
        if (canDie) {
            Life -= damage;
            healthbar.transform.localScale = new Vector2(Mathf.Clamp(ExtensionMethods.Remap(Life, 0, maxLife, 0, 3), 0, 3), 45);
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("boss_1_hit");
            if (Life <= 0) {
                StartCoroutine(die());
                canDie = false;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision) {


        if (canHurt)
        {
            if (collision.tag == "Player")
            {
                player.GetComponent<Player>().DamagePlayer();
            }
        }
        if (collision.tag == "Bullet")
            {
                if (mode == Mode.GrivMode)
                {
                    print("Dotykam gracza");

                    Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                    collision.transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + 180);
                    collision.GetComponent<Rigidbody2D>().AddForce(direction * 20, ForceMode2D.Impulse);
                    collision.GetComponent<Bullet>().friendly = false;
                    takeDamage(collision.GetComponent<Bullet>().damage * 0.1f);
                }
                else
                {
                    GameObject.Destroy(collision.gameObject);
                    takeDamage(collision.GetComponent<Bullet>().damage);
                }
            }
        
        

    }

    


    public void StartHurting() {
        canHurt = true;
    }

    public void StopHurting() {
        canHurt = false;
    }

    private Vector3 LerpByDistance(Vector3 A, Vector3 B, float x) {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }

    IEnumerator die() {
        mode = Mode.Rest;
        healthbar.SetActive(false);
        animator.SetTrigger("Die");
        speed = 0;
        transform.Find("MinimapIcon").gameObject.SetActive(false);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("boss_1_sound");
        yield return new WaitForSeconds(.5f);
        GameObject.Find("SceneMenager").GetComponent<Bulid_Script>().BossDied();

        Instantiate(weaponsToLeave[Random.Range(0, weaponsToLeave.Length)], transform.position, Quaternion.identity);

        yield return new WaitForSeconds(10);

        Destroy(gameObject);
    }

    IEnumerator ChangeModeAfter(float time, Mode newMode) {
        yield return new WaitForSeconds(time);
        speed = 100;
        yield return new WaitForSeconds(1);
        mode = newMode;
        canChange = true;
    }

    IEnumerator WakeUp(float time) {
        yield return new WaitForSeconds(time);
        mode = Mode.ChaseTarget;
        canDie = true;
        string text = "Defeat Opponent";
        PlayerPrefs.SetString("CurrentTask", text);
        player.GetComponent<Player>().Notify(text, 3);
       
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
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }
}