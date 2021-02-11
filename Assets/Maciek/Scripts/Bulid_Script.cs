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
    public GameObject panelPart;
    public event Action<int> OnColorChange;

    [Header("Room 1")]
    public List<GameObject> platforms1;
    public List<GameObject> corridors1;
    public List<GameObject> enemies1;
    public GameObject bigPlatform1;
    public GameObject boss1;
    public Color bgColor1;


    [Header("Panel Size")]
    [Range(2, 9)]
    public int maxX;
    [Range(2, 9)]
    public int maxY;

    [Header("Mode")]
    [Range(1, 3)]
    public int mode;





    void Start()
    {
        var init = GetComponent<Przepis>().init;
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
        switch (mode) {
            case 1:
                init.SpawnBossLair(platforms1[1], bigPlatform1, corridors1);
                init.SpawnBoss(boss1);
                break;
            case 2:
                init.BulidPanel(maxX, maxY,panelPart);
                init.FindPlatfromPlaces();
                init.PlacePlatforms(platforms1);
                init.PlaceCorridors(corridors1);
                init.SpawnEnemies(enemies1);
                //init.SpawnLights(light);
                break;
            case 3:
                init.BulidFinalPhase(platforms1, bigPlatform1, corridors1);
                init.StartFinalPhase(enemies1);
                break;
        }
        init.SetBackground(bgColor1);
        init.SpawnPlayer(player);
        //Instantiate(player,new Vector2(0,0),Quaternion.identity);

        switch (PlayerPrefs.GetInt("GameId")) {

            case 1:

                break;
            case 2:

                break;
        }
    }
    public void BossDied() {
        var init = GetComponent<Przepis>().init;
        print("Boss Kaput");


        switch (PlayerPrefs.GetInt("GameId")) {

            case 1:

                break;
            case 2:

                break;
        }
    }
}
