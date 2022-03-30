using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject cam;
    public GameObject weapon;
    private Animator animator;
    public GameObject weaponRender;
    public float MoveSpeed = 0.1f;


    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        transform.position = new Vector2(transform.position.x + x * MoveSpeed, transform.position.y + y * MoveSpeed);

        Vector2 movement = new Vector2(x, y);
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10f), Time.deltaTime * 5f);


        // Funkcja narazie nie przydatna, ponieważ weapon to specyficzna broń na scenie Home ;o

        /* float odleglosc = Vector2.Distance(transform.position, weapon.transform.position);
        if (odleglosc < 0.7f) {  
            if (Input.GetKeyDown(KeyCode.F)){
                weapon.transform.parent = weaponRender.transform;
                //weapon.transform.position = new Vector3(weaponRender.transform.position.x, weaponRender.transform.position.y, -1);
                weapon.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f);
                //weapon.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        } */


    }

}

