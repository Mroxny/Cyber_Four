using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.Advertisements;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class Bulid_Script : MonoBehaviour
{
    private AudioManager am;

    [Header("General")]
    [Space(0)]
    public GameObject player;
    public GameObject panelPart;
    public GameObject interaction;
    public GameObject exitScript;
    public GameObject itemScript;
    public List<string> music;
    public event Action<int> OnColorChange;

    [Header("Room 1")]
    public List<GameObject> platforms1;
    public List<GameObject> corridors1;
    public List<GameObject> enemies1;
    public List<GameObject> bigPlatform1;
    public GameObject boss1;
    public Sprite exit1;
    public Sprite item1;
    public Color bgColor1;

    [Header("Room 2")]
    [Space(20)]
    public List<GameObject> platforms2;
    public List<GameObject> corridors2;
    public List<GameObject> enemies2;
    public List<GameObject> bigPlatform2;
    public GameObject boss2;
    public Sprite exit2;
    public Sprite item2;
    public Color bgColor2;

    [Header("Room 3")]
    [Space(20)]
    public List<GameObject> platforms3;
    public List<GameObject> corridors3;
    public List<GameObject> enemies3;
    public List<GameObject> bigPlatform3;
    public GameObject boss3;
    public Sprite exit3;
    public Sprite item3;
    public Color bgColor3;

    [Header("Room 4")]
    [Space(20)]
    public List<GameObject> platforms4;
    public List<GameObject> corridors4;
    public List<GameObject> enemies4;
    public List<GameObject> bigPlatform4;
    public GameObject boss4;
    public Sprite exit4;
    public Sprite item4;
    public Color bgColor4;

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

    void Awake()
    {
#if UNITY_IOS
        Advertisement.Initialize("3835253", false);
#elif UNITY_ANDROID
        Advertisement.Initialize("3835252", false);
#endif
        Application.targetFrameRate = 60;

    }
    void Start()
    {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        StopAll();
        BulidLevel();

        

    }

    public void PlaySound(string soundName)
    {
        am.Play(soundName);
    }
    public void StopAll()
    {
        am.StopAll();
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
                        AstarPath.active.Scan();
                        init.SpawnBoss(boss1);
                        PlaySound(music[0]);
                        PlaySound("background_1");
                        break;
                    case 2:
                        init.SpawnBossLair(platforms2[0], bigPlatform2[0], corridors2);
                        AstarPath.active.Scan();
                        init.SpawnBoss(boss2);
                        PlaySound(music[1]);
                        PlaySound("background_2");
                        break;
                    case 3:
                        init.SpawnBossLair(platforms3[0], bigPlatform3[0], corridors3);
                        AstarPath.active.Scan();
                        init.SpawnBoss(boss3);
                        PlaySound(music[2]);
                        PlaySound("background_3");
                        break;
                    case 4:
                        init.SpawnBossLair(platforms4[0], bigPlatform4[0], corridors4);
                        AstarPath.active.Scan();
                        init.SpawnBoss(boss4);
                        PlaySound(music[2]);
                        PlaySound("background_3");
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
                        AstarPath.active.Scan();
                        init.SpawnExit(interaction, exitScript);
                        init.SpawnEnemies(enemies1);
                        PlaySound(music[UnityEngine.Random.Range(3,5)]);
                        PlaySound("background_1");
                        break;
                    case 2:
                        init.PlacePlatforms(platforms2);
                        init.PlaceCorridors(corridors2);
                        AstarPath.active.Scan();
                        init.SpawnExit(interaction, exitScript);
                        init.SpawnEnemies(enemies2);
                        PlaySound(music[UnityEngine.Random.Range(3, 5)]);
                        PlaySound("background_2");
                        break;
                    case 3:
                        init.PlacePlatforms(platforms3);
                        init.PlaceCorridors(corridors3);
                        AstarPath.active.Scan();
                        init.SpawnExit(interaction, exitScript);
                        init.SpawnEnemies(enemies3);
                        PlaySound(music[UnityEngine.Random.Range(3, 5)]);
                        PlaySound("background_3");
                        break;
                    case 4:
                        init.PlacePlatforms(platforms4);
                        init.PlaceCorridors(corridors4);
                        AstarPath.active.Scan();
                        init.SpawnExit(interaction, exitScript);

                        List<GameObject> enemies = new List<GameObject>();
                        enemies.AddRange(enemies1);
                        enemies.AddRange(enemies2);
                        enemies.AddRange(enemies3);
                        enemies.AddRange(enemies4);

                        init.SpawnEnemies(enemies);
                        PlaySound(music[UnityEngine.Random.Range(3, 5)]);
                        PlaySound("background_3");
                        break;
                }
                break;
            case 3:
                switch (room) {
                    case 1:
                        init.BulidFinalPhase(platforms1, bigPlatform1[1], corridors1);
                        AstarPath.active.Scan();
                        init.SpawnExit(interaction, itemScript);
                        StartCoroutine(WaveSpawner(enemies1,1, 30));
                        PlaySound(music[UnityEngine.Random.Range(5, music.Count)]);
                        PlaySound("background_1");
                        break;
                    case 2:
                        init.BulidFinalPhase(platforms2, bigPlatform2[1], corridors2);
                        AstarPath.active.Scan();
                        init.SpawnExit(interaction, itemScript);
                        StartCoroutine(WaveSpawner(enemies2, 3, 30));
                        PlaySound(music[UnityEngine.Random.Range(5, music.Count)]);
                        PlaySound("background_2");
                        break;
                    case 3:
                        init.BulidFinalPhase(platforms3, bigPlatform3[1], corridors3);
                        AstarPath.active.Scan();
                        init.SpawnExit(interaction, itemScript);
                        StartCoroutine(WaveSpawner(enemies3, 3, 30));
                        PlaySound(music[UnityEngine.Random.Range(5, music.Count)]);
                        PlaySound("background_3");
                        break;
                    case 4:
                        init.BulidFinalPhase(platforms3, bigPlatform3[1], corridors3);
                        AstarPath.active.Scan();
                        init.SpawnExit(interaction, itemScript);

                        List<GameObject> enemies = new List<GameObject>();
                        enemies.AddRange(enemies1);
                        enemies.AddRange(enemies2);
                        enemies.AddRange(enemies3);
                        enemies.AddRange(enemies4);

                        StartCoroutine(WaveSpawner(enemies, 3, 30));
                        PlaySound(music[UnityEngine.Random.Range(5, music.Count)]);
                        PlaySound("background_3");
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
            case 4:
                init.SetBackground(bgColor4);
                break;
        }

        player = init.SpawnPlayer(player);
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

    IEnumerator WaveSpawner(List<GameObject> enemies, int waves, float delayTime) {
        var init = GetComponent<Przepis>().init;
        string text = "";
        for (int i = 1; i <= waves; i++) {
            yield return new WaitForSeconds(delayTime);
            if (i == 1) {
                text = "The first wave is coming";
            }
            else if (i == 2) {
                text = "The second wave is coming";
            }
            else if (i == 3) {
                text = "The third wave is coming";
            }
            else if (i > 3) {
                text = "Another wave is coming";
            }
            if (i == waves) {
            StartCoroutine(EnemiesCheck(3f));
            }
            player.GetComponent<Player>().Notify(text, 4);
            StartCoroutine(init.StartFinalPhase(enemies));
        }
        
    }

    IEnumerator EnemiesCheck(float time) {
        bool hasEnemies = true;
        while (hasEnemies) {
            yield return new WaitForSeconds(time);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
                hasEnemies = false;
                string finalText = "Go to the item";
                player.GetComponent<Player>().Notify(finalText, 4);
            }
        }
    }
    public void BossDied() {
        var init = GetComponent<Przepis>().init;
        init.SpawnExit(interaction,exitScript);
        StartCoroutine(Notify());
    }
}
