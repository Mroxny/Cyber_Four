using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class MainMenu_SM : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject playerSelect;
    public GameObject settings;
    public GameObject single_ContinuePanel;
    public GameObject single_aus;
    public GameObject loadingScreen;
    int characterId;
    public GameObject[] objects;
    public AudioMixer audioMixer;
    public Animator animator;
    private AudioManager am;

    public event Action<int> OnColorChange;

    void Awake() {

#if UNITY_IOS
        Advertisement.Initialize("3835253", false);
#elif UNITY_ANDROID
        Advertisement.Initialize("3835252", false);
#endif
        //objects = GameObject.FindGameObjectsWithTag("colorchange");
        Application.targetFrameRate = -1;


    }
    bool oneTime = true;

    // Start is called before the first frame update
    public void Start() {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        mainMenu.gameObject.SetActive(true);
        playerSelect.gameObject.SetActive(false);
        single_ContinuePanel.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        loadingScreen.gameObject.SetActive(false);
        single_aus.gameObject.SetActive(false);

        if (oneTime) {
            if (PlayerPrefs.HasKey("Music_Volume")) {
                SetMusic(PlayerPrefs.GetFloat("Music_Volume"));
            }
            if (PlayerPrefs.HasKey("SFX_Volume")) {
                SetSFX(PlayerPrefs.GetFloat("SFX_Volume"));
            }
            StopAll();
            PlaySound("MainTheme");
            oneTime = false;
        }
        if (!PlayerPrefs.HasKey("ColorId")) {
            PlayerPrefs.SetInt("ColorId", 1);
        }


    }

    public void PlaySound(string soundName) {
        am.Play(soundName);
    }
    public void StopAll()
    {
        am.StopAll();
    }
    public void NewGame() {

        PlayerPrefs.DeleteKey("ModeId");
        PlayerPrefs.DeleteKey("GameId");
        PlayerPrefs.DeleteKey("CharacterId");
        PlayerPrefs.DeleteKey("ModeCounter");
        PlayerPrefs.DeleteKey("Garry");
        PlayerPrefs.DeleteKey("Intro");
        SaveSystem.ResetValues();
        Start();

    }

    public void AreYouSure() {
        single_ContinuePanel.gameObject.SetActive(false);
        single_aus.gameObject.SetActive(true);
    }

    public void ContinueSingle() {
        GetComponent<LevelLoader>().LoadLevel(2);
    }

    public void LoadLevelSingle() {
        if (!PlayerPrefs.HasKey("CharacterId")) {
            PlayerPrefs.SetInt("CharacterId", characterId);
        }
        mainMenu.gameObject.SetActive(false);
        playerSelect.gameObject.SetActive(false);
        single_ContinuePanel.gameObject.SetActive(false);
        single_aus.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        loadingScreen.gameObject.SetActive(true);
        GetComponent<LevelLoader>().LoadLevel(1);
    }
    public void LoadLevelMulti() {
        mainMenu.gameObject.SetActive(false);
        playerSelect.gameObject.SetActive(false);
        single_ContinuePanel.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        single_aus.gameObject.SetActive(false);
        loadingScreen.gameObject.SetActive(true);
        GetComponent<LevelLoader>().LoadLevel(2);
    }


    public void PlaySingle() {
        
        if (PlayerPrefs.HasKey("CharacterId")) {
            mainMenu.gameObject.SetActive(false);
            playerSelect.gameObject.SetActive(false);
            single_aus.gameObject.SetActive(false);
            single_ContinuePanel.gameObject.SetActive(true);
            if (PlayerPrefs.HasKey("ModeId")) {
                single_ContinuePanel.transform.Find("Continue").gameObject.SetActive(true);
            }
            else {
                single_ContinuePanel.transform.Find("Continue").gameObject.SetActive(false);
            }
            settings.gameObject.SetActive(false);
        }
        else {
            playerSelect.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
            playerSelect.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            playerSelect.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            playerSelect.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
            playerSelect.transform.GetChild(5).gameObject.SetActive(false);           //GetComponent<Button>().interactable = false;
            mainMenu.gameObject.SetActive(false);
            playerSelect.gameObject.SetActive(true);
            animator.SetTrigger("PlayCharacterSelect");
            characterId = 0;
        }
    }
    public void PlayMulti() {
        LoadLevelMulti();
    }
    public void SetCharacter(int id) {
        characterId = id;
        playerSelect.transform.GetChild(5).gameObject.SetActive(true); //GetComponent<Button>().interactable = true;
        switch (id) {
            case 1:
                playerSelect.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                playerSelect.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
                playerSelect.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
                playerSelect.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 2:
                playerSelect.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                playerSelect.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
                playerSelect.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
                playerSelect.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 3:
                playerSelect.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                playerSelect.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
                playerSelect.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
                playerSelect.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 4:
                playerSelect.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                playerSelect.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
                playerSelect.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
                playerSelect.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(true);
                break;
        }

    }



    public void SetMusic(float volume) {
        audioMixer.SetFloat("Volume_music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music_Volume", volume);
    }

    public void SetSFX(float volume) {
        audioMixer.SetFloat("Volume_sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }


    public void Setcolor(int color) {
        OnColorChange?.Invoke(color);
        PlayerPrefs.SetInt("ColorId", color);

    }

    public void Vibrations(bool v) {
        if (v == true) {
            PlayerPrefs.SetInt("Vibrations", 1);
        }
        else {
            PlayerPrefs.SetInt("Vibrations", 0);
        }
    }

    public void Settings() {
        mainMenu.gameObject.SetActive(false);
        playerSelect.gameObject.SetActive(false);
        single_ContinuePanel.gameObject.SetActive(false);
        single_aus.gameObject.SetActive(false);
        settings.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("Vibrations") == 1) {
            GameObject.Find("Vibrations_Toggle").GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        }
        else {
            GameObject.Find("Vibrations_Toggle").GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        }
        if (PlayerPrefs.HasKey("Music_Volume")) {
            GameObject.Find("Audio_Slider").transform.Find("SliderMusic").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music_Volume");
        }
        if (PlayerPrefs.HasKey("SFX_Volume")) {
            GameObject.Find("Audio_Slider").transform.Find("SliderSFX").GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFX_Volume");
        }
    }

    public void Quit() {
        Application.Quit();
    }



    void Update() {

    }
}
