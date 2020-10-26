using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour {
    public void Update() {


        
            float moveSpeed = 100f;
            Vector3 shootDir = new Vector3(1,0,0);
            transform.position += shootDir * moveSpeed * Time.deltaTime;
        
    }
}
