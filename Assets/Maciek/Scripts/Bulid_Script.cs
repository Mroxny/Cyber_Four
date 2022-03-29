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
    public List<string> music;
    public string[] tasks;
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

    [Header("Room 4")]
    [Space(20)]
    public List<GameObject> platforms4;
    public List<GameObject> corridors4;
    public List<GameObject> enemies4;
    public List<GameObject> bigPlatform4;
    public GameObject boss4;
    public Color bgColor4;

    [Header("Panel Size")]
    [Range(2, 9)]
    public int maxX;
    [Range(2, 9)]
    public int maxY;

    [Header("Mode")]
    [Range(1, 3)]
    public int mode;
    

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
                        StartCoroutine(StartFinalPhaseDelay(enemies1, 8));
                        PlaySound(music[UnityEngine.Random.Range(5, music.Count)]);
                        PlaySound("background_1");
                        break;
                    case 2:
                        init.BulidFinalPhase(platforms2, bigPlatform2[1], corridors2);
                        AstarPath.active.Scan();
                        StartCoroutine(StartFinalPhaseDelay(enemies2, 15, boss2, 1, 10));
                        PlaySound(music[UnityEngine.Random.Range(5, music.Count)]);
                        PlaySound("background_2");
                        break;
                    case 3:
                        init.BulidFinalPhase(platforms3, bigPlatform3[1], corridors3);
                        AstarPath.active.Scan();
                        StartCoroutine(StartFinalPhaseDelay(enemies3, 20, boss3, 1, 10));
                        PlaySound(music[UnityEngine.Random.Range(5, music.Count)]);
                        PlaySound("background_3");
                        break;
                    case 4:
                        init.BulidFinalPhase(platforms3, bigPlatform3[1], corridors3);
                        AstarPath.active.Scan();

                        List<GameObject> enemies = new List<GameObject>();
                        enemies.AddRange(enemies1);
                        enemies.AddRange(enemies2);
                        enemies.AddRange(enemies3);
                        enemies.AddRange(enemies4);

                        StartCoroutine(StartFinalPhaseDelay(enemies, 30));
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
                PlayerPrefs.SetString("CurrentTask", tasks[0]);
                player.GetComponent<Player>().Notify(tasks[0], 4);
                break;
            case 2:
                PlayerPrefs.SetString("CurrentTask", tasks[0]);
                player.GetComponent<Player>().Notify(tasks[0], 4);
                break;
            case 3:
                PlayerPrefs.SetString("CurrentTask", tasks[1]);
                player.GetComponent<Player>().Notify(tasks[1], 4);
                break;
        }
    }

    IEnumerator StartFinalPhaseDelay(List<GameObject> enemies, float delayTime) {
        
        yield return new WaitForSeconds(delayTime);

        var init = GetComponent<Przepis>().init;
        string text = "Survive!";

        InvokeRepeating("EnemiesCheck", .6f, 3f);
        PlayerPrefs.SetString("CurrentTask", text);
        player.GetComponent<Player>().Notify(text, 4);
        StartCoroutine(init.StartFinalPhase(enemies));

    }

    IEnumerator StartFinalPhaseDelay(List<GameObject> enemies, int enemiesNum, GameObject boss, int bossNum, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        var init = GetComponent<Przepis>().init;
        string text = "Survive!";

        InvokeRepeating("EnemiesCheck", .6f, 3f);
        PlayerPrefs.SetString("CurrentTask", text);
        player.GetComponent<Player>().Notify(text, 4);
        StartCoroutine(init.StartFinalPhase(enemies, enemiesNum, boss, bossNum));

    }

    void EnemiesCheck() {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && GameObject.FindGameObjectsWithTag("Boss").Length <= 0)
        {
                CancelInvoke();

                var init = GetComponent<Przepis>().init;

                PlayerPrefs.SetString("CurrentTask", tasks[2]);
                player.GetComponent<Player>().Notify(tasks[2], 4);
                init.SpawnExit(interaction, exitScript);

            // mark this level as finished
            if (PlayerPrefs.HasKey("FinishedLevels") && !PlayerPrefs.GetString("FinishedLevels").Contains(PlayerPrefs.GetInt("GameId").ToString()))
                PlayerPrefs.SetString("FinishedLevels", PlayerPrefs.GetString("FinishedLevels") + PlayerPrefs.GetInt("GameId"));


        }
    }
    public void BossDied() {
        if (mode == 1) {
            var init = GetComponent<Przepis>().init;
            init.SpawnExit(interaction, exitScript);
            StartCoroutine(Notify());
        }
        
    }
}
