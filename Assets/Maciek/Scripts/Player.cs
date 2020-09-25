using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

   
    void Start()
    {
        SetSkin(); 
    }
    void SetSkin() {
        
        switch (PlayerPrefs.GetInt("CharacterId")) {
            case 1:
                transform.GetChild(1-1).gameObject.SetActive(true);
                break;
            case 2:
                transform.GetChild(2-1).gameObject.SetActive(true);
                break;
            case 3:
                transform.GetChild(3-1).gameObject.SetActive(true);
                break;
            case 4:
                transform.GetChild(4-1).gameObject.SetActive(true);
                break;
        }
        
    }
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
        print(PlayerPrefs.GetInt("CharacterId"));
    }
}
