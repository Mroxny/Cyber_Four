using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject cam;
    private Animator animator;
    public GameObject weaponRender;
    public bool canMove = true;
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
    private AudioManager am;


    public Joystick movementJoystick;
    public Joystick aimJoystick;
    private bool canDamage = true;

    public Vector2 movement;
    public Vector2 mouse;
    private float lookDir;
    private GameObject weapon;

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
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        SetChar();

    }


   public void SetChar() {
        SetLife();
        StartCoroutine(SpawnPlayer());
        InvokeRepeating("FPS", 0.1f, 0.5f);
    }
    public void SetLife() {
        int health = 0;
        switch (PlayerPrefs.GetInt("CharacterId")) {
            case 1:
                health = 6;
                break;
            case 2:
                health = 4;
                break;
            case 3:
                health = 4;
                break;
            case 4:
                health = 4;
                break;
        }

        Slider slider = gameObject.transform.Find("Canvas").transform.Find("Health").transform.Find("HealthBar").GetComponent<Slider>();
        slider.maxValue = health;
        slider.value = health;
        gameObject.transform.Find("Canvas").transform.Find("Health").transform.Find("HP").GetComponent<TextMeshProUGUI>().text = slider.value.ToString();
    }
    public void DamagePlayer() {
        if (canDamage) {
            Slider slider = gameObject.transform.Find("Canvas").transform.Find("Health").transform.Find("HealthBar").GetComponent<Slider>();
            slider.value = slider.value - 1;
            gameObject.transform.Find("Canvas").transform.Find("Health").transform.Find("HP").GetComponent<TextMeshProUGUI>().text = slider.value.ToString();
            PlaySound("hit_player");
            if (slider.value <= 0) {
                StartCoroutine(Die());
            }
            canDamage = false;
            StartCoroutine(DamageCooldown(.5f));
        }
    }
    private IEnumerator DamageCooldown(float time) {
        yield return new WaitForSeconds(time);
        canDamage = true;
    }
    public void HealPlayer(int healthPoints) {

    }
    public IEnumerator Die() {
        canMove = false;
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(2);
        GameObject.Find("SceneMenager").GetComponent<LevelLoader>().LoadLevel(1);
        
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


    public void ShowTask() {
        Notify(PlayerPrefs.GetString("CurrentTask"), 5);
    }

    public void Notify(string text, float duration) {
        taskNotifierHandler.SetActive(true);
        taskNotifierHandler.GetComponent<TaskHandler>().Notify(text, duration);
    }

    public void PlaySound(string soundName) {
        am.Play(soundName);
    }

    void FPS() {
        int fps = (int)(1f / Time.unscaledDeltaTime); ;
        GameObject.Find("FPS").GetComponent<TextMeshProUGUI>().text = fps.ToString();
    }
    bool oneTime = true;
    void Update() {

        if (canMove) {
            
            if (movementJoystick.Direction.sqrMagnitude == 0) {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                am.StopPlaying("Footstep_Player");
                oneTime = true;
            }
            else {
                if (oneTime) {
                    PlaySound("Footstep_Player");
                    oneTime = false;
                }
                movement.x = movementJoystick.Horizontal;
                movement.y = movementJoystick.Vertical;

            }
            mouse = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            if (movement.x != 0) {
                if (aimJoystick.Horizontal == 0) {
                    lookDir = movement.x;
                }
                else {
                    lookDir = aimJoystick.Horizontal;
                }
            }
        }

        
        if (GameObject.Find("CC_Count") != null) {
            GameObject.Find("CC_Count").GetComponent<TextMeshProUGUI>().text = cyberCoin.ToString();
        }


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
        if (canMove) {
            rb.position = new Vector2(transform.position.x + movement.x * MoveSpeed, transform.position.y + movement.y * MoveSpeed);

            if (animator != null && (!GetComponentInChildren<WeaponInteract>() || GetComponentInChildren<WeaponInteract>().canFire)) {
                animator.SetFloat("Horizontal", lookDir);
                animator.SetFloat("Speed", movement.sqrMagnitude);
                //print(lookDir);
                if (GetComponentInChildren<WeaponInteract>()) {
                    weapon = GetComponentInChildren<WeaponInteract>().gameObject;
                    /*if (lookDir < 0) weapon.gameObject.transform.localScale = new Vector3(weapon.transform.localScale.x * -1f, weapon.transform.localScale.y, weapon.transform.localScale.z);
                    else if (lookDir > 0) weapon.gameObject.transform.localScale = new Vector3(weapon.transform.localScale.x * -1f, weapon.transform.localScale.y, weapon.transform.localScale.z);*/
                }
            }
            else if (animator != null && !GetComponentInChildren<WeaponInteract>().canFire) {
                animator.SetFloat("Horizontal", GetComponentInChildren<WeaponInteract>().lookDir.x);
            }
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10f), Time.deltaTime * 5f);
        }
    }

}
