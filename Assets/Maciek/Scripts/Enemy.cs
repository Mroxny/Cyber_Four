using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Life;
    public float speed;

    public void takeDamage(float damage) {
        Life -= damage;
        Debug.Log(Life);
        if (Life <= 0) {
            die();
        }
    }

    private void die() {
        Destroy(this.gameObject);
    }
}
