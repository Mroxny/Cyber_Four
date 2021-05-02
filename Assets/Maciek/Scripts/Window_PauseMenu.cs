using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Window_PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settings;
    public GameObject areYouSure;
    public GameObject sceneMenager;
    public Animator animator;
    public AudioMixer audioMixer;

    private Camera cam;
    private AudioManager am;
    
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
        MenuPanel();
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
    public void SetMusic(float volume) {
        audioMixer.SetFloat("Volume_music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music_Volume", volume);
    }

    public void SetSFX(float volume) {
        audioMixer.SetFloat("Volume_sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }
    public void SetVibrations(bool v) {
        if (v == true) {
            PlayerPrefs.SetInt("Vibrations", 1);
        }
        else {
            PlayerPrefs.SetInt("Vibrations", 0);
        }
    }
    public void MenuPanel() {
        pauseMenu.SetActive(true);
        settings.SetActive(false);
        areYouSure.SetActive(false);
    }
    public void SettingsPanel() {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
        areYouSure.SetActive(false);
        if (PlayerPrefs.GetInt("Vibrations") == 1) {
            settings.gameObject.transform.Find("Vibrations_Toggle").GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        }
        else {
            settings.gameObject.transform.Find("Vibrations_Toggle").GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        }
        if (PlayerPrefs.HasKey("Music_Volume")) {
            settings.gameObject.transform.Find("SliderMusic").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music_Volume");
        }
        if (PlayerPrefs.HasKey("SFX_Volume")) {
            settings.gameObject.transform.Find("SliderSFX").GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFX_Volume");
        }
    }
    int lvl = 0;
    public void AreYouSure(int level) {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        areYouSure.SetActive(true);
        lvl = level;
    }
    public void LoadLevel() {
        sceneMenager.GetComponent<LevelLoader>().LoadLevel(lvl);
        Time.timeScale = 1;
    }
    public void Close() {
        animator.SetTrigger("close");
        StartCoroutine(DisableAfterTime(1.4f));
    }
    IEnumerator DisableAfterTime(float time) {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
        Time.timeScale = 1;
        GameObject.Find("Player(Clone)").GetComponent<Player>().EnableHUD();
    }
}
