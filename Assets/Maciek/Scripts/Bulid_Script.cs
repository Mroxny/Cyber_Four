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
    public List<GameObject> bigPlatform1;
    public GameObject boss1;
    public Color bgColor1;

    [Header("Room 2")]
    [Space(20)]
    public List<GameObject> platforms2;
    public List<GameObject> corridors2;
    public List<GameObject> enemies2;
    public List<GameObject> bigPlatform2;
    public GameObject boss2;
    public Color bgColor2;

    [Header("Room 3")]
    [Space(20)]
    public List<GameObject> platforms3;
    public List<GameObject> corridors3;
    public List<GameObject> enemies3;
    public List<GameObject> bigPlatform3;
    public GameObject boss3;
    public Color bgColor3;

    [Header("Panel Size")]
    [Range(2, 9)]
    public int maxX;
    [Range(2, 9)]
    public int maxY;

    [Header("Mode")]
    [Range(1, 3)]
    public int mode;
    public string task1;
    public string task2;
    public string task3;

    [Header("Room")]
    [Range(1, 3)]
    public int room;



    void Start()
    {
        var init = GetComponent<Przepis>().init;
        BulidLevel();

        

    }
    public void BulidLevel() {
        if (PlayerPrefs.HasKey("GameId")) {
            room = PlayerPrefs.GetInt("GameId");
        }
        if (PlayerPrefs.HasKey("ModeId")) {
            mode = PlayerPrefs.GetInt("ModeId");
        }
        var init = GetComponent<Przepis>().init;
        switch (mode) {
            case 1:
                switch (room) {
                    case 1:
                        init.SpawnBossLair(platforms1[0], bigPlatform1[0], corridors1);
                        init.SpawnBoss(boss1);
                        break;
                    case 2:
                        init.SpawnBossLair(platforms2[0], bigPlatform2[0], corridors2);
                        init.SpawnBoss(boss2);
                        break;
                    case 3:
                        init.SpawnBossLair(platforms3[0], bigPlatform3[0], corridors3);
                        init.SpawnBoss(boss3);
                        break;
                }
                break;
            case 2:
                init.BulidPanel(maxX, maxY, panelPart);
                init.FindPlatfromPlaces();
                switch (room) {
                    case 1:
                        init.PlacePlatforms(platforms1);
                        init.PlaceCorridors(corridors1);
                        init.SpawnEnemies(enemies1);
                        break;
                    case 2:
                        init.PlacePlatforms(platforms2);
                        init.PlaceCorridors(corridors2);
                        init.SpawnEnemies(enemies2);
                        break;
                    case 3:
                        init.PlacePlatforms(platforms3);
                        init.PlaceCorridors(corridors3);
                        init.SpawnEnemies(enemies3);
                        break;
                }
                break;
            case 3:
                switch (room) {
                    case 1:
                        init.BulidFinalPhase(platforms1, bigPlatform1[1], corridors1);
                        init.StartFinalPhase(enemies1);
                        break;
                    case 2:
                        init.BulidFinalPhase(platforms2, bigPlatform2[1], corridors2);
                        init.StartFinalPhase(enemies2);
                        break;
                    case 3:
                        init.BulidFinalPhase(platforms3, bigPlatform3[1], corridors3);
                        init.StartFinalPhase(enemies3);
                        break;
                }
                
                break;
        }
        switch (room) {
            case 1:
                init.SetBackground(bgColor1);
                break;
            case 2:
                init.SetBackground(bgColor2);
                break;
            case 3:
                init.SetBackground(bgColor3);
                break;
        }

        player = init.SpawnPlayer(player);
        print("player spawned");
        StartCoroutine(Notify());
        
    }
    IEnumerator Notify() {
        yield return new WaitForEndOfFrame();
        switch (mode) {
            case 1:
                PlayerPrefs.SetString("CurrentTask", task1);
                player.GetComponent<Player>().Notify(task1, 4);
                break;
            case 2:
                PlayerPrefs.SetString("CurrentTask", task2);
                player.GetComponent<Player>().Notify(task2, 4);
                break;
            case 3:
                PlayerPrefs.SetString("CurrentTask", task3);
                player.GetComponent<Player>().Notify(task3, 4);
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
