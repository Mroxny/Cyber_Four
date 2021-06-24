using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Window_Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textPlace;
    public Image spriteSocket;
    public Animator animator;
    public GameObject sceneMenager;

    private AudioManager am;
    private string[] text;
    private int index = 0;
    private bool finished =true;

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

    public void SetText(string[] t) {
        text = t;
        textPlace.text = "";
        index = 0;
        spriteSocket.sprite = GetComponentInParent<SpriteRenderer>().sprite;
        StartCoroutine(Wait(.9f));
    }

    public void Continue() {
        
    if (finished) {
                if (index < text.Length) {
                 StartCoroutine(TypeSentence(text[index]));
                    index++;
                 }
                else {
                    Close();
                    index = 0;
                }
    }
    else {
                StopAllCoroutines();
                textPlace.text = text[index-1];
                finished = true;
    }   
    }
    private IEnumerator TypeSentence(string t) {
        textPlace.text = "";
        foreach (char letter in t.ToCharArray()) {
            finished = false;
            textPlace.text += letter;
            yield return null;
        }
        finished = true;
    }

    private IEnumerator Wait(float time) {
        yield return new WaitForSecondsRealtime(time);
        Continue();
    }

    public void Close() {
        animator.SetTrigger("close");
        StartCoroutine(DisableAfterTime(1.3f));
    }
    IEnumerator DisableAfterTime(float time) {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
        Time.timeScale = 1;
        GameObject.Find("Player(Clone)").GetComponent<Player>().EnableHUD();
    }
}
