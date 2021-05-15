using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Window_Intro : MonoBehaviour
{
    public List<GameObject> panels;
    public GameObject dot;
    public GameObject nextButton;
    public GameObject prevButton;
    public Animator animator;
    public GameObject sceneMenager;
    
    private AudioManager am;
    private int currentPanel = 0;
    private List<GameObject> dots = new List<GameObject>();

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
        GameObject.Find("Player(Clone)").GetComponent<Player>().DisableHUD();
        if (sceneMenager == null) {

            sceneMenager = GameObject.Find("/SceneMenager");
            print(sceneMenager);
        }
        if (GameObject.Find("TaskNotifier(Clone)") != null) {
            GameObject.Find("TaskNotifier(Clone)").GetComponent<TaskHandler>().HideNote();
        }
        Time.timeScale = 0;
        SetDots();
        panels[0].SetActive(true);
        dots[0].GetComponent<CanvasGroup>().alpha = 1f;
        UpdateButtons();
    }
    
    private void SetDots() {
        foreach (GameObject i in panels) {
            GameObject d = Instantiate(dot) as GameObject;
            d.SetActive(true);
            d.transform.SetParent(dot.transform.parent, false);
            d.GetComponent<CanvasGroup>().alpha = .3f;
            dots.Add(d);
        }
    }

    private void UpdateButtons() {
        if (currentPanel + 1 < panels.Count) {
            nextButton.SetActive(true);
        }
        else {
            nextButton.SetActive(false);
        }
        if (currentPanel - 1 >= 0) {
            prevButton.SetActive(true);
        }
        else {
            prevButton.SetActive(false);
        }
    }

    public void Next() { 
        if (currentPanel+1 < panels.Count) {
            currentPanel++;
            panels[currentPanel - 1].SetActive(false);
            panels[currentPanel].SetActive(true);
            dots[currentPanel - 1].GetComponent<CanvasGroup>().alpha = .3f;
            dots[currentPanel].GetComponent<CanvasGroup>().alpha = 1f;
        }
        UpdateButtons();
    }
    public void Previous() {
        if (currentPanel -1 >= 0) {
            currentPanel--;
            panels[currentPanel + 1].SetActive(false);
            panels[currentPanel].SetActive(true);
            dots[currentPanel + 1].GetComponent<CanvasGroup>().alpha = .3f;
            dots[currentPanel].GetComponent<CanvasGroup>().alpha = 1f;
        }
        UpdateButtons();
    }


    public void Close() {
        animator.SetTrigger("close");
        StartCoroutine(TurnOffList(.5f));
        StartCoroutine(DisableAfterTime(1.4f));
    }
    IEnumerator TurnOffList(float time) {
        yield return new WaitForSecondsRealtime(time);
        foreach (Transform child in dot.transform.parent) {
            child.gameObject.SetActive(false);
        }
    }
    IEnumerator DisableAfterTime(float time) {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
        currentPanel = 1;
        Time.timeScale = 1;
        GameObject.Find("Player(Clone)").GetComponent<Player>().EnableHUD();
    }
}
