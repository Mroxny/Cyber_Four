using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlABLA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        GetComponent<Transform>().position = new Vector2(transform.position.x+x,transform.position.y+y);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z+(x*100));
    }
}
