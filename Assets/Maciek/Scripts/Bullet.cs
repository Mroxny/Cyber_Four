using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public bool friendly;
    private void OnTriggerEnter2D(Collider2D collision){

        EnemyAI enemy = collision.GetComponent<EnemyAI>();
        Player player = collision.GetComponent<Player>();
        WeaponInteract owner = collision.GetComponent<WeaponInteract>();
        Bullet twin = collision.GetComponent<Bullet>();

        if (owner != null) {
            return;
        }
        if (twin != null) {
            return;
        }
        if (collision.gameObject.layer == 11) {
            //ciapak work here
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("hitWall");
            GameObject.Destroy(gameObject);
        }
        if (friendly) {
                if (enemy != null) {
                    print("Enemy Hit");
                    enemy.TakeDamage(damage);
                    GameObject.Destroy(gameObject);
                }
                if (collision.tag == "Boss") {
                    return;
                }
                if (player != null) {
                    return;
                }
        }
        else {
                if (player != null) {
                    player.DamagePlayer();
                    GameObject.Destroy(gameObject);
                }
                if (enemy != null) {
                    return;
                }
        }
            //print(collision.name);
            GameObject.Destroy(gameObject);
        
    }
}
