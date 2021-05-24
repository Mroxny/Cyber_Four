using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class TimerHandler : MonoBehaviour
{
    public GameObject mapCounter;
    public GameObject timer;
    private int minutes=0;
    private int seconds=0;
    private string time = "";
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) {
            InvokeRepeating("UpdateTime", 1f, 1f);
            string text;
            text = PlayerPrefs.GetInt("GameId").ToString() + "-";
            if (PlayerPrefs.GetInt("ModeId") == 2) {
                int num = 2 + PlayerPrefs.GetInt("ModeCounter");
                text += num.ToString();
            }
            else if (PlayerPrefs.GetInt("ModeId") == 3) {
                int num = 2 + PlayerPrefs.GetInt("ModeId");
                text += num.ToString();
            }
            else {
                text += PlayerPrefs.GetInt("ModeId").ToString();
            }
            mapCounter.GetComponent<TextMeshProUGUI>().text = text;
        }
    }
    private void UpdateTime() {
        if (seconds < 59) {
            seconds += 1;
        }
        else {
            minutes += 1;
            seconds = 0;
        }

        time = minutes.ToString();
        time += ":";

        if (seconds < 10) {
            time += 0 + seconds.ToString();
        }
        else {
            time += seconds.ToString();
        }
        timer.GetComponent<TextMeshProUGUI>().text = time;
    }

}
