﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject reloadButton;
    public Rigidbody2D rb;
    public List<GameObject> disabledInHome;
    public List<GameObject> HUDElements;
    public GameObject pauseMenu;
    public GameObject taskNotifier;
    public GameObject spawnDust;
    public GameObject diedWindow;
    public Joystick movementJoystick;
    public Joystick aimJoystick;
    public int[] unlockedWeapons;
    public int[] currentWeapons;

    public int cyberCoin;
    [HideInInspector]
    public GameObject cam;
    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public GameObject gunSlot1;
    [HideInInspector]
    public GameObject gunSlot2;
    [HideInInspector]
    public Vector2 movement;
    [HideInInspector]
    public float lookDir;
    [HideInInspector]
    public GameObject weaponRender;
    [HideInInspector]

    private Animator animator;
    private float MoveSpeed;
    private int ability;
    private AudioManager am;
    private bool canDamage = true;
    private GameObject gunSlotButton1;
    private GameObject gunSlotButton2;
    private GameObject pauseMenuHandler;
    private GameObject taskNotifierHandler;

    void Start() {

        GameObject.Find("FPS").GetComponent<TextMeshProUGUI>().text = Application.version;


        if (cam == null) {
            cam = GameObject.Find("Main Camera");
        }
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            foreach (GameObject j in disabledInHome) {
                j.SetActive(false);
            }
        }
        taskNotifierHandler = Instantiate(taskNotifier, new Vector2(0, 0), Quaternion.identity);
        if (!SaveSystem.IsValid() || SaveSystem.LoadPlayer() == null) {
            currentWeapons = new int[2];
            unlockedWeapons = new int[2];
            currentWeapons[0] = 0;
            currentWeapons[1] = 1;
            unlockedWeapons[0] = 0;
            unlockedWeapons[1] = 1;
            SaveSystem.SavePlayer(this);
        }
        currentWeapons = SaveSystem.LoadPlayer().cw;
        unlockedWeapons = SaveSystem.LoadPlayer().uw;
        cyberCoin = SaveSystem.LoadPlayer().cyberCoin;
        taskNotifierHandler.SetActive(false);
        gunSlotButton1 = transform.Find("Canvas").transform.Find("GunSlots").transform.Find("GunSlotButton_1").gameObject;
        gunSlotButton2 = transform.Find("Canvas").transform.Find("GunSlots").transform.Find("GunSlotButton_2").gameObject;
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        SetChar();
    }


   public void SetChar() {
        SetLife();
        StartCoroutine(SpawnPlayer());
    }
    public void SetLife() {
        int health = 0;
        switch (PlayerPrefs.GetInt("CharacterId")) {
            case 1:
                health = 8;
                break;
            case 2:
                health = 12;
                break;
            case 3:
                health = 4;
                break;
            case 4:
                health = 16;
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
            StartCoroutine(cam.GetComponent<CameraShake>().Shake(0.1f, 0.1f, transform));
            if (PlayerPrefs.GetInt("Vibrations") == 1) {
                Handheld.Vibrate();
            }
            if (slider.value <= 0) {
                StartCoroutine(Die());
                return;
            }
            canDamage = false;
            StartCoroutine(DamageCooldown(.5f));
        }
    }
    private IEnumerator DamageCooldown(float time) {
        yield return new WaitForSeconds(time);
        canDamage = true;
    }
    public bool HealPlayer(int healthPoints) {
        Slider slider = gameObject.transform.Find("Canvas").transform.Find("Health").transform.Find("HealthBar").GetComponent<Slider>();
        if (slider.value + healthPoints <= slider.maxValue) {
            slider.value += healthPoints;
            gameObject.transform.Find("Canvas").transform.Find("Health").transform.Find("HP").GetComponent<TextMeshProUGUI>().text = slider.value.ToString();
            return true;
        }
        return false;


    }
    public IEnumerator Die() {
        canMove = false;
        canDamage = false;
        aimJoystick.enabled = false;
        animator.SetTrigger("Die");
        animator.SetFloat("Speed", 0);
        PlayerPrefs.DeleteKey("ModeId");
        PlayerPrefs.DeleteKey("ModeCounter");
        yield return new WaitForSeconds(2);
        Instantiate(diedWindow);
        
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
                MoveSpeed = 0.08f;
                ability = 2;
                //weaponRender = transform.GetChild(2 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 3:
                transform.GetChild(3 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(3 - 1).transform.GetChild(0).GetComponent<Animator>();
                GameObject.Find("CharProf").transform.GetChild(0).transform.GetChild(3 - 1).gameObject.SetActive(true);
                weaponRender = GameObject.Find("WeaponRender3");
                GameObject.Find("CharProf3").SetActive(true);
                MoveSpeed = 0.14f;
                ability = 3;
                //weaponRender = transform.GetChild(3 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 4:
                transform.GetChild(4 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(4 - 1).transform.GetChild(0).GetComponent<Animator>();
                GameObject.Find("CharProf").transform.GetChild(0).transform.GetChild(4 - 1).gameObject.SetActive(true);
                weaponRender = GameObject.Find("WeaponRender4");
                GameObject.Find("CharProf4").SetActive(true);
                MoveSpeed = 0.06f;
                ability = 4;
                //weaponRender = transform.GetChild(4 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
                
        }
        Instantiate(spawnDust, transform.position, Quaternion.identity);
        PlaySound("spawn");
        StartCoroutine(cam.GetComponent<CameraShake>().Shake(0.15f,0.2f,transform));
        InventoryDataBase db = GameObject.Find("Inventory").GetComponent<InventoryDataBase>();
        SetWeaponInSlot(Instantiate(db.GetWeapon(SaveSystem.LoadPlayer().cw[0])), 1);
        SetWeaponInSlot(Instantiate(db.GetWeapon(SaveSystem.LoadPlayer().cw[1])), 2);
        ChangeCurrentGunSlot(1);
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

    public int GetCurrentGunSlot() {
        if (gunSlot1.activeSelf) {
            return 1;
        }
        else if (gunSlot2.activeSelf) {
            return 2;
        }
        else {
            return 0;
        }
    }


    public void SetWeaponInSlot(GameObject weapon,int slot) {
        
        if (slot == 1 && weapon.GetComponent<WeaponInteract>() != null) {
            if (gunSlot1 != null) {
                gunSlot1.transform.SetParent(null);
                gunSlot1.transform.position = weapon.transform.position;
                gunSlot1.GetComponent<WeaponInteract>().InPlayerHands = false;
                gunSlot1.GetComponent<WeaponInteract>().inHand = false;
            }
            weapon.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f);
            weapon.transform.SetParent(gameObject.transform);
            weapon.GetComponent<WeaponInteract>().InPlayerHands = true;
            weapon.GetComponent<WeaponInteract>().inHand = true;
            weapon.GetComponent<WeaponInteract>().canFire = true;
            gunSlot1 = weapon;
            currentWeapons[0] = GameObject.Find("Inventory").GetComponent<InventoryDataBase>().GetWeaponId(gunSlot1);
            GameObject icon = gunSlotButton1.transform.Find("WeaponIcon").gameObject;
            icon.GetComponent<Image>().sprite = GameObject.Find("Inventory").GetComponent<InventoryDataBase>().GetWeaponIcon(currentWeapons[0]);
            float iconWidth = GameObject.Find("Inventory").GetComponent<InventoryDataBase>().GetWeaponIcon(currentWeapons[0]).rect.width;
            float iconHeight = GameObject.Find("Inventory").GetComponent<InventoryDataBase>().GetWeaponIcon(currentWeapons[0]).rect.height;
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(ExtensionMethods.Remap(iconWidth, 0, 20, 0, 50), ExtensionMethods.Remap(iconHeight, 0, 20, 0, 50));
        }
        else if (slot == 2 && weapon.GetComponent<WeaponInteract>() != null) {
            if (gunSlot2 != null) {
                gunSlot2.transform.SetParent(null);
                gunSlot2.transform.position = weapon.transform.position;
                gunSlot2.GetComponent<WeaponInteract>().InPlayerHands = false;
                gunSlot2.GetComponent<WeaponInteract>().inHand = false;
            }
            weapon.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f);
            weapon.transform.SetParent(gameObject.transform);
            weapon.GetComponent<WeaponInteract>().InPlayerHands = true;
            weapon.GetComponent<WeaponInteract>().inHand = true;
            weapon.GetComponent<WeaponInteract>().canFire = true;
            gunSlot2 = weapon;
            currentWeapons[1] = GameObject.Find("Inventory").GetComponent<InventoryDataBase>().GetWeaponId(gunSlot2);
            GameObject icon = gunSlotButton2.transform.Find("WeaponIcon").gameObject;
            icon.GetComponent<Image>().sprite = GameObject.Find("Inventory").GetComponent<InventoryDataBase>().GetWeaponIcon(currentWeapons[1]);
            float iconWidth = GameObject.Find("Inventory").GetComponent<InventoryDataBase>().GetWeaponIcon(currentWeapons[1]).rect.width ;
            float iconHeight = GameObject.Find("Inventory").GetComponent<InventoryDataBase>().GetWeaponIcon(currentWeapons[1]).rect.height ;
            icon.GetComponent<RectTransform>().sizeDelta=new Vector2(ExtensionMethods.Remap(iconWidth, 0, 20, 0, 50), ExtensionMethods.Remap(iconHeight, 0, 20, 0, 50)); 
        }
        SaveSystem.SavePlayer(this);
    }
    public void ChangeCurrentGunSlot(int slot) {
        if (slot == 1) {
            gunSlot1.SetActive(true);
            gunSlotButton1.transform.Find("Border").gameObject.SetActive(true);
            gunSlot2.SetActive(false);
            gunSlotButton2.transform.Find("Border").gameObject.SetActive(false);
            if (!gunSlot1.GetComponent<WeaponInteract>().IsGun) reloadButton.GetComponent<Button>().interactable = false;
            else {
                reloadButton.GetComponent<Button>().interactable = true;
                if (gunSlot1.GetComponent<WeaponInteract>().currentAmmo <=0) {
                    gunSlot1.GetComponent<WeaponInteract>().canFire = true;
                    Reload();
                }
            }
        }
        else if (slot == 2) {
            gunSlot1.SetActive(false);
            gunSlotButton1.transform.Find("Border").gameObject.SetActive(false);
            gunSlot2.SetActive(true);
            gunSlotButton2.transform.Find("Border").gameObject.SetActive(true);
            if (!gunSlot2.GetComponent<WeaponInteract>().IsGun) reloadButton.GetComponent<Button>().interactable = false;
            else {
                reloadButton.GetComponent<Button>().interactable = true;
                if (gunSlot2.GetComponent<WeaponInteract>().currentAmmo <= 0) {
                    gunSlot2.GetComponent<WeaponInteract>().canFire = true;
                    Reload();
                }
            }
        }
    }

    public void Reload() {
        if (gunSlot1.activeSelf == true) {
            gunSlot1.GetComponent<WeaponInteract>().Reload();
            StartCoroutine(DisableButtonForTime(reloadButton.GetComponent<Button>(), gunSlot1.GetComponent<WeaponInteract>().reloadTime));
        }
        else if (gunSlot2.activeSelf == true) {
            gunSlot2.GetComponent<WeaponInteract>().Reload();
            StartCoroutine(DisableButtonForTime(reloadButton.GetComponent<Button>(), gunSlot2.GetComponent<WeaponInteract>().reloadTime));
        }
    }
    private IEnumerator DisableButtonForTime(Button button,float time) {
        button.interactable = false;
        yield return new WaitForSeconds(time);
        button.interactable = true;
    }


    public void test() {

        SaveSystem.ResetValues();

        /*currentWeapons = new int[2];
        unlockedWeapons = new int[2];
        currentWeapons[0] = 0;
        currentWeapons[1] = 1;
        unlockedWeapons[0] = 0;
        unlockedWeapons[1] = 1;
        SaveSystem.SavePlayer(this);*/

        //print(SaveSystem.LoadPlayer().uw[0] + " " + SaveSystem.LoadPlayer().uw[1]);


    }

    bool oneTime = true;
    void Update() {

        

        
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
            if (movement.x != 0) {
                lookDir = movement.x;
            }
            if (aimJoystick.Direction.sqrMagnitude>0) {
                lookDir = aimJoystick.Direction.x;
            }

            
            rb.position = new Vector2(transform.position.x + movement.x * MoveSpeed, transform.position.y + movement.y * MoveSpeed);
        }
        if (animator !=null) {
            animator.SetFloat("Horizontal", lookDir);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10f), Time.deltaTime * 5f);

        string text="";
        if (gunSlot1.GetComponent<WeaponInteract>().IsGun) {
            text = gunSlot1.GetComponent<WeaponInteract>().currentAmmo + "/" + gunSlot1.GetComponent<WeaponInteract>().ammo;
        }
        else {
            text = "-";
        }
        gunSlotButton1.transform.Find("Ammo").GetComponent<TextMeshProUGUI>().text = text;


        if (gunSlot2.GetComponent<WeaponInteract>().IsGun) {
            text = gunSlot2.GetComponent<WeaponInteract>().currentAmmo + "/" + gunSlot2.GetComponent<WeaponInteract>().ammo;
        }
        else {
            text = "-";
        }
        gunSlotButton2.transform.Find("Ammo").GetComponent<TextMeshProUGUI>().text = text;
    }

}
