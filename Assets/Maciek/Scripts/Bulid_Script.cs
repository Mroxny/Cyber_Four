using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;

public class Bulid_Script : MonoBehaviour
{
    [Header("Player Prefab")]
    [Space(0)]
    public GameObject player;
    
    
    [Header("Room 1")]
    public List<GameObject> platforms1;
    public List<GameObject> corridors1;
    public List<GameObject> enemies1;
    public GameObject boss1;
    public GameObject bossPlatform1;


    [Header("Panel Size")]
    [Range(2, 9)]
    public int maxX;
    [Range(2, 9)]
    public int maxY;





    void Start()
    {
        BulidLevel();





        switch (PlayerPrefs.GetInt("GameId")) {

            case 1:

                break;
            case 2:

                break;
        }
    }
    public void BulidLevel() {
        var init = GetComponent<Przepis>().init;
        //init.BulidPanel(maxX,maxY);
        //init.FindPlatfromPlaces();
        //init.PlacePlatforms(platforms1);
        //init.PlaceCorridors(corridors1);
        //init.SpawnEnemies(enemies1);
        init.SpawnBossLair(platforms1[0], bossPlatform1,corridors1);
        init.SpawnBoss(boss1);
        init.SpawnPlayer(player);
    }
    public void BossDied() {

        print("Boss Kaput");


        switch (PlayerPrefs.GetInt("GameId")) {

            case 1:

                break;
            case 2:

                break;
        }
    }
}
