using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;

public class Bulid_Script : MonoBehaviour
{
    public string[] enemies;

    void Start()
    {
        var init = GetComponent<Przepis>().init;

        init.BulidMap();
        
        for (int i=0; i<2; i++) {
           enemies[i] = (i + 1).ToString();
        }
        init.SpawnEnemies(enemies);
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
