using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public float Life;
    public float speed;
    private Bulid_Script bulidScript;

    private void Start() {
        GameObject sceneMenager = GameObject.FindWithTag("SceneMenager");
        bulidScript = sceneMenager.GetComponent<Bulid_Script>();
    }

    public void takeDamage(float damage) {
        Life -= damage;
        Debug.Log(Life);
        if (Life <= 0) {
            die();
        }
    }

    private void die() {
        Destroy(this.gameObject);
        bulidScript.BossDied();
    }
}