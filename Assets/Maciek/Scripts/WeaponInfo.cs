using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public GameObject bullet;
    public float velocity;
    public float damage;
    public GameObject weapon;
    public Transform firepoint;
    public bool IsGun;
    public AudioClip shoot;
    private AudioSource audioPlayer;
    private Player player;
    private bool inHand = false;
    private bool onetime = true;

    void Start(){
        audioPlayer = GetComponent<AudioSource>();
    }
    private void trigger()
    {
        Vector2 mouse = player.mouse;
        Vector2 lookDir = mouse - new Vector2(player.transform.position.x, player.transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        firepoint.rotation = Quaternion.Euler(new Vector3(mouse.x,mouse.y,angle));
        
        GameObject shot = Instantiate(bullet, new Vector2(firepoint.position.x, firepoint.position.y), Quaternion.Euler(new Vector3(0,0,angle + 90f)));
        shot.GetComponent<Bullet>().damage = damage;
        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * velocity, ForceMode2D.Impulse);
        audioPlayer.PlayOneShot(shoot);
    }

    private void SetPlayer()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (onetime) {
            SetPlayer();
            onetime = false;
        }
        float odleglosc = Vector2.Distance(player.transform.position, transform.position);
        if (odleglosc < 0.7f)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("mam bron");
                weapon.transform.parent = player.weaponRender.transform;
                //weapon.transform.position = new Vector3(weaponRender.transform.position.x, weaponRender.transform.position.y, -1);
                weapon.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.2f);
                //weapon.GetComponent<SpriteRenderer>().sortingOrder = 1;
                inHand = true;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (inHand)
            {
                trigger();
            }
        }
    }

}
