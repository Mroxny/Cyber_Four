using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Window_MissionSelect : MonoBehaviour
{
    public Animator animator;
    public GameObject sceneMenager;
    public GameObject[] finishedMarks;

    private AudioManager am;


    private Camera cam;
    void OnEnable() {
        Initiate();
    }
    public void PlaySound(string soundName)
    {
        am.Play(soundName);
    }
    public void StopSound(string soundName)
    {
        am.StopPlaying(soundName);
    }

    public void Initiate() {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cam = Camera.main;
        gameObject.transform.Find("Canvas").GetComponent<Canvas>().worldCamera = cam;
        GameObject.Find("Player(Clone)").GetComponent<Player>().DisableHUD();

        SetFinishedMissions();

        if (sceneMenager == null) {
            
            sceneMenager = GameObject.Find("/SceneMenager");
            print(sceneMenager);
        }
        if (GameObject.Find("TaskNotifier(Clone)") != null) {
            GameObject.Find("TaskNotifier(Clone)").GetComponent<TaskHandler>().HideNote();
        }
        Time.timeScale = 0;
    }

    private void SetFinishedMissions()
    {

        if (PlayerPrefs.HasKey("FinishedLevels") && PlayerPrefs.GetString("FinishedLevels").Contains('1'))
            finishedMarks[0].SetActive(true);
        if (PlayerPrefs.HasKey("FinishedLevels") && PlayerPrefs.GetString("FinishedLevels").Contains('2'))
            finishedMarks[1].SetActive(true);
        if (PlayerPrefs.HasKey("FinishedLevels") && PlayerPrefs.GetString("FinishedLevels").Contains('3'))
            finishedMarks[2].SetActive(true);

    }

    public void LoadLevel(int gameId) {
        PlayerPrefs.SetInt("GameId", gameId);
        PlayerPrefs.SetInt("ModeId", 1);
        PlayerPrefs.DeleteKey("ModeCounter");
        sceneMenager.GetComponent<LevelLoader>().LoadLevel(2);
        Time.timeScale = 1;
    }
    
    public void Close() {
        animator.SetTrigger("close");
        StartCoroutine(DisableAfterTime(1.4f));
       // GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Click");
    }
    IEnumerator DisableAfterTime(float time) {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
        Time.timeScale = 1;
        GameObject.Find("Player(Clone)").GetComponent<Player>().EnableHUD();
    }
}
