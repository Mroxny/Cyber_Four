using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System;
using UnityEngine.UIElements;
using Mirror.Examples.Additive;
//using UnityEditor.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject cam;
    private Animator animator;
    public GameObject weaponRender;
    private float MoveSpeed;
    private int ability;
    public Camera camera = new Camera();
    public Rigidbody2D rb;

    public Vector2 movement;
    public Vector2 mouse;
    private Vector2 lookDir;
    

    void Start() {
        if (cam == null) {
            cam = GameObject.Find("Main Camera");
        }
        if (camera == null) {
            camera = Camera.main;
        }
        SetChar();

    }
    void SetChar() {
        
        switch (PlayerPrefs.GetInt("CharacterId")) {
            case 1:
                transform.GetChild(1 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(1 - 1).transform.GetChild(0).GetComponent<Animator>();
                weaponRender = GameObject.Find("WeaponRender1");
                MoveSpeed = 0.1f;
                ability = 1;
                //weaponRender = transform.GetChild(1 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 2:
                transform.GetChild(2 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(2 - 1).transform.GetChild(0).GetComponent<Animator>();
                weaponRender = GameObject.Find("WeaponRender2");
                MoveSpeed = 0.1f;
                ability = 2;
                //weaponRender = transform.GetChild(2 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 3:
                transform.GetChild(3 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(3 - 1).transform.GetChild(0).GetComponent<Animator>();
                weaponRender = GameObject.Find("WeaponRender3");
                MoveSpeed = 0.1f;
                ability = 3;
                //weaponRender = transform.GetChild(3 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 4:
                transform.GetChild(4 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(4 - 1).transform.GetChild(0).GetComponent<Animator>();
                weaponRender = GameObject.Find("WeaponRender4");
                MoveSpeed = 0.1f;
                ability = 4;
                //weaponRender = transform.GetChild(4 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
        }

    }

    void isDodging() {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void isDashing() {
    
    }

    void isBulletProof() {
        
    }

    void isHealing() {
    
    }

    void Update() {
            
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mouse = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        lookDir = mouse - new Vector2(transform.position.x, transform.position.y);

        if (Input.GetKeyDown(KeyCode.Space)) {
                switch (ability) {
                    case 1:
                    isDodging();
                        break;  
                    case 2:
                    isDashing();
                        break;  
                    case 3:
                    isHealing();
                        break;
                    case 4:
                    isBulletProof();
                        break;
                }   
            }
            // Funkcja narazie nie przydatna, ponieważ weapon to specyficzna broń na scenie Home ;o
            
    }
    private void FixedUpdate()
    {
        rb.position = new Vector2(transform.position.x + movement.x * MoveSpeed, transform.position.y + movement.y * MoveSpeed);
        animator.SetFloat("Horizontal", lookDir.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10f), Time.deltaTime * 5f);
    }

}
