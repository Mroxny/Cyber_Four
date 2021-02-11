using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Life;
    public float speed;
    public float viewAngle;
    public float viewRadius;

    [HideInInspector]
    public bool IsPlayerVisible = false;
    [HideInInspector]
    public GameObject player;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsPlayerVisible) {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            Debug.Log(distance);
            if (distance < 1.5) {
                follow(player.transform);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name.Equals("Player(Clone)")) {
            player = collision.gameObject;
            IsPlayerVisible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Player(Clone)"))
        {
            player = null;
            IsPlayerVisible = false;
        }
    }

    private void follow( Transform player) {
        rb.AddForce((player.transform.position - transform.position).normalized * speed);
    }

    private Vector3 DirFromAngle(float angleInDeg) {
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }

    public void takeDamage(float damage) {
        Life -= damage;
        Debug.Log(Life);
        if (Life <= 0) {
            die();
        }
    }

    private void die() {
        Destroy(this.gameObject);
    }

    void FixedUpdate() { 
        
    }
}
