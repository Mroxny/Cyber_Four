using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TMPro;


public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
public class Home_SM : MonoBehaviour {
    
    public GameObject playerPrefab;
    public List<GameObject> backgrounds;
    public GameObject cloud;
    public GameObject intro;



    void Awake() {
#if UNITY_IOS
        Advertisement.Initialize("3835253", false);
#elif UNITY_ANDROID
        Advertisement.Initialize("3835252", false);
#endif
        Application.targetFrameRate = 60;

    }
    void Start() {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().StopAll();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("HomeTheme");
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("engine");
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("engine");
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("engine");
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("engine");

        PlayerPrefs.DeleteKey("ModeId");
        PlayerPrefs.DeleteKey("ModeCounter");

        Vector2 spawnPos = new Vector2(12, 0);
        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        float color = Random.Range(100, 256);
        color = color.Remap(0,255,0,1);
        foreach (GameObject j in backgrounds) {
            j.GetComponent<SpriteRenderer>().color = new Color(color, color, color);
        }
        int randRange = Random.Range(7,13);
        for (int i=0;i<=randRange;i++) {
            Instantiate(cloud,new Vector2(75,0), Quaternion.identity);
        }
        if (!PlayerPrefs.HasKey("Intro")) {
            PlayerPrefs.SetInt("Intro", 0);
            StartCoroutine(StartIntro(1.3f));
        }
        
    }
    private IEnumerator StartIntro(float time) {
        yield return new WaitForSeconds(time);
        Instantiate(intro);
    }
    // Update is called once per frame
    void Update() {

    }
}
