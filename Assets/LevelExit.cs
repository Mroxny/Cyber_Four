using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("ModeId") == 1) {
            PlayerPrefs.SetInt("ModeId", 2);
            GameObject.Find("SceneMenager").GetComponent<LevelLoader>().LoadLevel(2);
        }
        else if (PlayerPrefs.GetInt("ModeId") == 2) {
            if (!PlayerPrefs.HasKey("ModeCounter")) {
                PlayerPrefs.SetInt("ModeCounter", 1);
                GameObject.Find("SceneMenager").GetComponent<LevelLoader>().LoadLevel(2);
            }
            else if (PlayerPrefs.GetInt("ModeCounter") == 1) {
                PlayerPrefs.SetInt("ModeCounter", 2);
                GameObject.Find("SceneMenager").GetComponent<LevelLoader>().LoadLevel(2);
            }
            else if (PlayerPrefs.GetInt("ModeCounter") == 2) {
                PlayerPrefs.DeleteKey("ModeCounter");
                PlayerPrefs.SetInt("ModeId", 3);
                GameObject.Find("SceneMenager").GetComponent<LevelLoader>().LoadLevel(2);
            }
            
        }
        else if (PlayerPrefs.GetInt("ModeId") == 3) {
            GameObject.Find("SceneMenager").GetComponent<LevelLoader>().LoadLevel(1);

        }

    }

}
