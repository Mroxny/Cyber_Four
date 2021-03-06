using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System;
using UnityEngine.UIElements;
using Mirror.Examples.Additive;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject cam;
    private Animator animator;
    public GameObject weaponRender;
    private float MoveSpeed;
    private int ability;
    public Camera camera = new Camera();
    public Rigidbody2D rb;
    public List<GameObject> disabledInHome;
    public List<GameObject> HUDElements;
    public GameObject sceneMenu;

    public Joystick joystick;
 

    public Vector2 movement;
    public Vector2 mouse;
    public bool withJoystick=true;
    private float lookDir;

    GameObject s;
    void Start() {
        if (cam == null) {
            cam = GameObject.Find("Main Camera");
        }
        if (camera == null) {
            camera = Camera.main;
        }
        SetChar();
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            foreach (GameObject j in disabledInHome) {
                j.SetActive(false);
            }
        }
        s = Instantiate(sceneMenu, new Vector2(0, 0), Quaternion.identity);
    }
    void SetChar() {
        
        switch (PlayerPrefs.GetInt("CharacterId")) {
            case 1:
                transform.GetChild(1 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(1 - 1).transform.GetChild(0).GetComponent<Animator>();
                GameObject.Find("CharProf").transform.GetChild(0).transform.GetChild(1 - 1).gameObject.SetActive(true);
                weaponRender = GameObject.Find("WeaponRender1");
                MoveSpeed = 0.1f;
                ability = 1;
                //weaponRender = transform.GetChild(1 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 2:
                transform.GetChild(2 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(2 - 1).transform.GetChild(0).GetComponent<Animator>();
                GameObject.Find("CharProf").transform.GetChild(0).transform.GetChild(2 - 1).gameObject.SetActive(true);
                weaponRender = GameObject.Find("WeaponRender2");
                GameObject.Find("CharProf2").SetActive(true);
                MoveSpeed = 0.1f;
                ability = 2;
                //weaponRender = transform.GetChild(2 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 3:
                transform.GetChild(3 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(3 - 1).transform.GetChild(0).GetComponent<Animator>();
                GameObject.Find("CharProf").transform.GetChild(0).transform.GetChild(3 - 1).gameObject.SetActive(true);
                weaponRender = GameObject.Find("WeaponRender3");
                GameObject.Find("CharProf3").SetActive(true);
                MoveSpeed = 0.1f;
                ability = 3;
                //weaponRender = transform.GetChild(3 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 4:
                transform.GetChild(4 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(4 - 1).transform.GetChild(0).GetComponent<Animator>();
                GameObject.Find("CharProf").transform.GetChild(0).transform.GetChild(4 - 1).gameObject.SetActive(true);
                weaponRender = GameObject.Find("WeaponRender4");
                GameObject.Find("CharProf4").SetActive(true);
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

    public void DisableHUD() {
        foreach (GameObject e in HUDElements) {
            e.SetActive(false);
        }  
    }
    public void EnableHUD() {
        foreach (GameObject e in HUDElements) {
            e.SetActive(true);
        }
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            foreach (GameObject j in disabledInHome) {
                j.SetActive(false);
            }
        }
    }
    public void FindScene() {
        s.SetActive(true);
        

    }




    void Update() {
            
        
        if (!withJoystick) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
        }
        mouse = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (movement.x!=0) {
            lookDir = movement.x;                                                        //mouse - new Vector2(transform.position.x, transform.position.y);
        }
        //mprint(lookDir);

        // -- Maciek was here
        int fps = (int)(1f / Time.unscaledDeltaTime); ;
        GameObject.Find("FPS").GetComponent<TextMeshProUGUI>().text = fps.ToString(); ;
        // -- 

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
        animator.SetFloat("Horizontal", lookDir);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10f), Time.deltaTime * 5f);
    }

}
