using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : MonoBehaviour {

    public GameObject cam;
    public GameObject weapon;
    private Animator animator;
    public GameObject weaponRender;

    void Start() {
        if (cam == null) {
            cam = GameObject.Find("Main Camera");
        }
       weapon = GameObject.Find("TestGun");
       
        SetChar();

    }
    void SetChar() {
        
        switch (PlayerPrefs.GetInt("CharacterId")) {
            case 1:
                transform.GetChild(1 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(1 - 1).transform.GetChild(0).GetComponent<Animator>();
                weaponRender = GameObject.Find("WeaponRender1");
                //weaponRender = transform.GetChild(1 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 2:
                transform.GetChild(2 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(2 - 1).transform.GetChild(0).GetComponent<Animator>();
                weaponRender = GameObject.Find("WeaponRender2");
                //weaponRender = transform.GetChild(2 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 3:
                transform.GetChild(3 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(3 - 1).transform.GetChild(0).GetComponent<Animator>();
                weaponRender = GameObject.Find("WeaponRender3");
                //weaponRender = transform.GetChild(3 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
            case 4:
                transform.GetChild(4 - 1).gameObject.SetActive(true);
                animator = transform.GetChild(4 - 1).transform.GetChild(0).GetComponent<Animator>();
                weaponRender = GameObject.Find("WeaponRender4");
                //weaponRender = transform.GetChild(4 - 1).transform.GetChild(0).GetComponent<GameObject>();
                break;
        }

    }
    
    void Update() {
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.position = new Vector2(transform.position.x + (x*0.1f), transform.position.y + (y * 0.1f));

        Vector2 movement = new Vector2(x,y);
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y,-10f), Time.deltaTime*5f);
       
        if (Input.GetKeyDown(KeyCode.F)) {
            
            weapon.transform.parent = weaponRender.transform;
            //weapon.transform.position = new Vector3(weaponRender.transform.position.x, weaponRender.transform.position.y, -1);
            weapon.transform.localPosition = new Vector3(0, 0, -1);
        }
    }
    
}
