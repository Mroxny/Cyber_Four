using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;

public class Bulid_Script : MonoBehaviour
{
    [Header("Room 1")]
    [Space(0)]
    public GameObject player;
    public List<GameObject> bottomPlatform;
    public List<GameObject> topPlatform;
    public List<GameObject> leftPlatform;
    public List<GameObject> rightPlatform;
    public List<GameObject> crossPlatform;
    public List<GameObject> Platform;
    public string[] enemies;
    
    



    void Start()
    {
        var init = GetComponent<Przepis>().init;

        init.LoadPlatforms(bottomPlatform, topPlatform);
        init.BulidMap();
        init.SpawnEnemies(enemies);
        Instantiate(player, new Vector3(0,0,0), Quaternion.identity);


        switch (PlayerPrefs.GetInt("GameId")) {

            case 1:

                break;
            case 2:

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
