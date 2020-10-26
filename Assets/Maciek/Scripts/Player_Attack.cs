using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    void Start()
    {
        //bullet= GameObject.Find("TestBullet");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
           Transform bulletTransform =Instantiate(bullet, transform.position, Quaternion.identity);

            Vector3 shootDir = new Vector3(1, 0, 0).normalized;
            //bulletTransform.GetComponent<TestBullet>().Setup(shootDir);
        }
    }
}
