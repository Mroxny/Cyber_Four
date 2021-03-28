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
    public int cyberCoin;
    public Camera camera = new Camera();
    public Rigidbody2D rb;
    public List<GameObject> disabledInHome;
    public List<GameObject> HUDElements;
    public GameObject pauseMenu;
    public GameObject taskNotifier;
    public GameObject spawnDust;

    public Joystick movementJoystick;
    public Joystick aimJoystick;
    public bool test = false;


    public Vector2 movement;
    public Vector2 mouse;
    private float lookDir;

    GameObject pauseMenuHandler;
    GameObject taskNotifierHandler;
    void Start() {
        if (cam == null) {
            cam = GameObject.Find("Main Camera");
        }
        if (camera == null) {
            camera = Camera.main;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            foreach (GameObject j in disabledInHome) {
                j.SetActive(false);
            }
        }
        taskNotifierHandler = Instantiate(taskNotifier, new Vector2(0, 0), Quaternion.identity);
        if (!SaveSystem.IsValid()) {
            SaveSystem.SavePlayer(this);
        }
            taskNotifierHandler.SetActive(false);
        cyberCoin = SaveSystem.LoadPlayer().cyberCoin;
        SetChar();
    }


    void SetChar() {
        StartCoroutine(SpawnPlayer());
    }
    IEnumerator SpawnPlayer() {
        yield return new WaitForSecondsRealtime(1f);
        if (PlayerPrefs.GetInt("Vibrations") == 1) {
            Handheld.Vibrate();
        }
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
        Instantiate(spawnDust, transform.position, Quaternion.identity);
        StartCoroutine(cam.GetComponent<CameraShake>().Shake(0.15f,0.2f,transform));
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
    public void Pause() {
        //musze zmienic sorki
        if (pauseMenuHandler)
        {
            pauseMenuHandler.SetActive(true);
        }
        else
        {
            pauseMenuHandler = Instantiate(pauseMenu, new Vector2(0, 0), Quaternion.identity);
            pauseMenuHandler.SetActive(true);
        }
    }
    public void TestButton() {
        if (!test) {
            test = true;
        }
        else{
            test = false;
        }
        SaveSystem.SavePlayer(this);
        //PlayerData data = SaveSystem.LoadPlayer();


    }


    public void ShowTask() {
        Notify(PlayerPrefs.GetString("CurrentTask"), 5);
    }

    public void Notify(string text, float duration) {
        taskNotifierHandler.SetActive(true);
        taskNotifierHandler.GetComponent<TaskHandler>().Notify(text, duration);
    }

    void Update() {
            
        
        if (movementJoystick.Direction.sqrMagnitude == 0) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else {
            movement.x = movementJoystick.Horizontal;
            movement.y = movementJoystick.Vertical;
        }
        mouse = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (movement.x!=0) {
            if (aimJoystick.Horizontal == 0) {
                lookDir = movement.x;                                                        //mouse - new Vector2(transform.position.x, transform.position.y);
            }
            else {
                lookDir = aimJoystick.Horizontal;
            }
        }
        

        // -- Maciek was here (FPS)
        int fps = (int)(1f / Time.unscaledDeltaTime); ;
        GameObject.Find("FPS").GetComponent<TextMeshProUGUI>().text = fps.ToString();
        if (GameObject.Find("CC_Count") != null) {
            GameObject.Find("CC_Count").GetComponent<TextMeshProUGUI>().text = cyberCoin.ToString();
        }
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
        if (animator != null) {
            animator.SetFloat("Horizontal", lookDir);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10f), Time.deltaTime * 5f);
        
    }

}
