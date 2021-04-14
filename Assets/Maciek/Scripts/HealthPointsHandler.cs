using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPointsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float pos = -26;
        for (int i = 1; i <= 3; i++) {
            print(i);
            GameObject bar = Instantiate(gameObject, new Vector2(pos, 0), Quaternion.identity);
            bar.transform.SetParent(GameObject.Find("Player(Clone)").transform.Find("Canvas").transform.Find("Health").transform);
            pos += 18;
        }
    }

    public void SetHealth(int life) {
        
    }
}
