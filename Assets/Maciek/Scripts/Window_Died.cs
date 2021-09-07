using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Window_Died : MonoBehaviour
{
    public Animator animator;
    public GameObject sceneMenager;
    private AudioManager am;


    private Camera cam;
    void OnEnable() {
        Initiate();
    }
    public void PlaySound(string soundName) {
        am.Play(soundName);
    }
    public void StopSound(string soundName) {
        am.StopPlaying(soundName);
    }

    public void Initiate() {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cam = Camera.main;
        gameObject.transform.Find("Canvas").GetComponent<Canvas>().worldCamera = cam;
        GameObject.Find("Player(Clone)").GetComponent<Player>().DisableHUD();
        if (sceneMenager == null) {

            sceneMenager = GameObject.Find("/SceneMenager");
            print(sceneMenager);
        }
        if (GameObject.Find("TaskNotifier(Clone)") != null) {
            GameObject.Find("TaskNotifier(Clone)").GetComponent<TaskHandler>().HideNote();
        }
        Time.timeScale = 0;
    }

    public void LoadLevel(int levelId) {
        sceneMenager.GetComponent<LevelLoader>().LoadLevel(levelId);
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
