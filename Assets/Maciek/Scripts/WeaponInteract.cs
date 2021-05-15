using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteract : MonoBehaviour
{
    public GameObject bullet;
    public Color bulletColor;
    public string weaponName;
    public float velocity;
    public float damage;
    public float fireRate;
    public string soundName = "TestGunShoot";
    public Transform firepoint;
    public Animator animator;
    public bool IsGun;
    public bool friendly = true;
    public GameObject player;
    public bool inHand = false;
    public bool aimAtPlayer = false;
    private AudioManager am;
    private bool canHurt = false;

    [HideInInspector]
    public Vector2 lookDir;

    private Vector2 playerLookDir;
    
    [HideInInspector]
    public bool canFire = true;
    
    [HideInInspector]
    public BoxCollider2D collider;
    [HideInInspector]
    public bool InPlayerHands = false;



    private void Start()
    {
            collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            RectTransform rt = (RectTransform)gameObject.transform;
 
            collider.size = new Vector2(rt.rect.width / 2, rt.rect.height / 2);
    }

    public void Shoot() {
        if (canFire) {
            StartCoroutine(trigger());
        }
    }

    private IEnumerator trigger() {
        
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        canFire = false;
        if (IsGun) {
            firepoint.rotation = Quaternion.Euler(new Vector3(lookDir.x, lookDir.y, angle));
            GameObject shot = Instantiate(bullet, new Vector2(firepoint.position.x, firepoint.position.y), Quaternion.Euler(new Vector3(0, 0, angle + 90f)));
            animator.SetTrigger("Fire");
            shot.GetComponent<Bullet>().damage = damage;
            shot.GetComponent<Bullet>().friendly = friendly;
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.AddForce(firepoint.up * velocity, ForceMode2D.Impulse);
            am.Play(soundName);
        }
        else {
            animator.SetTrigger("Attack");
            am.Play(soundName);
            StartCoroutine(BladeHurt(1));
        }
        

        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
    private IEnumerator BladeHurt(float time) {
        canHurt = true;
        yield return new WaitForSeconds(time);
        canHurt = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (canHurt) {
            if (friendly) {
                EnemyAI enemy = collision.GetComponent<EnemyAI>();
                if (enemy != null) {
                    enemy.TakeDamage(damage);
                }
            }
            else {
                Player player = collision.GetComponent<Player>();
                if (player != null) {
                    player.DamagePlayer();
                }
            }
        }
    }

    private void FixedUpdate() {
        if (player == null) {
            player = GameObject.Find("Player(Clone)");
            am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
        if (friendly) {
            if (InPlayerHands) {
                if (player.GetComponent<Player>().aimJoystick.Direction.sqrMagnitude > 0) {
                    lookDir = player.GetComponent<Player>().aimJoystick.Direction;
                }
                else{
                    lookDir = new Vector2(Mathf.Ceil(player.GetComponent<Player>().lookDir),0);
                }
                //print(player.GetComponent<Player>().lookDir);
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                
                    if (angle < 90 && angle > -90) {
                        //Debug.Log(angle);
                        if (transform.localScale.x < 0) transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                        transform.rotation = Quaternion.Euler(new Vector3(lookDir.x, lookDir.y, angle));
                    }
                    else {
                        if (transform.localScale.x > 0) transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                        transform.rotation = Quaternion.Euler(new Vector3(lookDir.x, lookDir.y, angle - 180f));
                    }
                
                
                if (player.GetComponent<Player>().aimJoystick.Direction.sqrMagnitude > 0 && canFire && inHand) {
                      Shoot();                    
                }
            }
        }
        else {
            if (aimAtPlayer) {
                lookDir = player.transform.position - transform.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                if (angle < 90 && angle > -90) {
                    if (transform.localScale.x < 0) transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                    transform.rotation = Quaternion.Euler(new Vector3(lookDir.x, lookDir.y, angle));
                }
                else {
                    if (transform.localScale.x > 0) transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                    transform.rotation = Quaternion.Euler(new Vector3(lookDir.x, lookDir.y, angle - 180f));
                }
            }
        }
    }

}
