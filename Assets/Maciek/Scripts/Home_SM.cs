using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TMPro;

public class Home_SM : MonoBehaviour {

    public GameObject playerPrefab;

    void Awake() {
#if UNITY_IOS
        Advertisement.Initialize("3835253", false);
#elif UNITY_ANDROID
        Advertisement.Initialize("3835252", false);
#endif
        Application.targetFrameRate = 60;

    }
    void Start() {
        Vector2 spawnPos = new Vector2(0, 0);
        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update() {

    }
}
