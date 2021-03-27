using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyAI enemy = collision.GetComponent<EnemyAI>();
        Player player = collision.GetComponent<Player>();

        /* Na sciany ktore zapewne będą w pokoju
        
        GameObject wall = collision.GetComponent<wall>();

        if (wall != null)
        {
            Destroy(GameObject);
        }*/

        if (enemy != null) {
            Debug.Log(damage);
            enemy.TakeDamage(damage);
        }
        if (player == null) {
            GameObject.Destroy(this.gameObject);
        }
        //GameObject.Destroy(this.gameObject);
        Debug.Log(collision.name);
    }
}
