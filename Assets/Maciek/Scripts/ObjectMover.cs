using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectMover : MonoBehaviour
{
    [Header("Direction")]
    public bool top = true;
    public bool left = false;
    public bool right = false;
    public bool down = false;

    [Space(30)]
    public float velocity = 1f;
    public bool repeat=false;
    public float repeatAfter = 20;
    public float backPoints;
    private Vector2 startPos;
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //GetComponent<SpriteRenderer>().material.mainTextureOffset = new Vector2(Time.time * velocity,0);
        if (top == true) {
            transform.Translate(Vector2.up * (Time.deltaTime * velocity));
        }
        if (left == true) {
            transform.Translate(Vector2.left * (Time.deltaTime * velocity));
        }
        if (right == true) {
            transform.Translate(Vector2.right * (Time.deltaTime * velocity));
        }
        if (down == true) {
            transform.Translate(Vector2.down * (Time.deltaTime * velocity));
        }
        if (repeat == true) {
            Vector2 distance = new Vector2(Mathf.Abs(startPos.x - transform.position.x),Mathf.Abs(startPos.y-transform.position.y));
            if (distance.x >= repeatAfter || distance.y >= repeatAfter) {
                transform.position=new Vector2(transform.position.x+backPoints,transform.position.y);
                distance = new Vector2(0,0);
            }
        }

    }
}

