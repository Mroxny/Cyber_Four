using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public GameObject bullet;
    public float velocity;
    public float damage;
    public float fireRate;
    public GameObject weapon;
    public Transform firepoint;
    public bool IsGun;
    private Player player;
    private bool inHand = false;
    private bool onetime = true;
    private Vector2 lookDir;
    private bool canFire = true;

    private IEnumerator trigger() {
        Vector2 mouse = player.mouse;
        //Vector2 lookDir = mouse - new Vector2(player.transform.position.x, player.transform.position.y); -- na potrzeby joysticka zmieniłem ~Maciek
        
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        //firepoint.rotation = Quaternion.Euler(new Vector3(mouse.x,mouse.y,angle));
        firepoint.rotation = Quaternion.Euler(new Vector3(lookDir.x, lookDir.y, angle));

        canFire = false;

        GameObject shot = Instantiate(bullet, new Vector2(firepoint.position.x, firepoint.position.y), Quaternion.Euler(new Vector3(0, 0, angle + 90f)));
        shot.GetComponent<Bullet>().damage = damage;
        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * velocity, ForceMode2D.Impulse);

        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    private void SetPlayer() {
        player = GameObject.FindObjectOfType<Player>();
    }
    private void Update() {
        if (onetime) {
            SetPlayer();
            onetime = false;
        }
        lookDir = player.GetComponent<Player>().aimJoystick.Direction;
        float odleglosc = Vector2.Distance(player.transform.position, transform.position);
        if (odleglosc < 0.7f) {
            if (Input.GetKeyDown(KeyCode.F) || player.GetComponent<Player>().test) {
                Debug.Log("mam bron");
                weapon.transform.parent = player.weaponRender.transform;
                //weapon.transform.position = new Vector3(weaponRender.transform.position.x, weaponRender.transform.position.y, -1);
                weapon.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.2f);
                //weapon.GetComponent<SpriteRenderer>().sortingOrder = 1;
                inHand = true;
            }
        }
        
        if ((lookDir.x != 0 || lookDir.y != 0) && canFire) {                         //(Input.GetMouseButtonDown(0)) {
            if (inHand) {
               StartCoroutine(trigger());
            }
        }
    }

}
