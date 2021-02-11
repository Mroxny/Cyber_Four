using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public List<Sprite> skins;
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        ObjectMover om = GetComponent<ObjectMover>();
        transform.position = new Vector2(Random.Range(35,81), Random.Range(-10, 11));
        sr.sprite = skins[Random.Range(0, skins.Count)];
        if (Random.Range(1, 6) == 1) {
            sr.flipX = true;
        }
        int randScale = 0;
        int randWayBack = 0;
        if (Random.Range(1, 5) == 1) {
            randScale = Random.Range(35, 41);
            transform.localScale = new Vector2(randScale, randScale);
            sr.sortingOrder = 20;
            om.velocity = Random.Range(5, 11);
            randWayBack = Random.Range(110, 200);
            om.repeatAfter = randWayBack;
            om.backPoints = randWayBack;
        }
        else {
            randScale = Random.Range(17, 21);
            transform.localScale = new Vector2(randScale, randScale);
            sr.sortingOrder = -20;
            om.velocity = Random.Range(1, 11);
            randWayBack = Random.Range(110, 200);
            om.repeatAfter = randWayBack;
            om.backPoints = randWayBack;
        }
            
        
    }


    void Update()
    {
        
    }
}
