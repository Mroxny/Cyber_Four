using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarryHandler : MonoBehaviour
{
    public GameObject dialog;
    public string[] d1;
    public string[] d2;



    void Start()
    {
        if (!PlayerPrefs.HasKey("Garry")) {
            PlayerPrefs.SetInt("Garry", 1);
        }


        if (PlayerPrefs.GetInt("Garry") == 1) {
            PlayerPrefs.SetInt("Garry", 0);

            GetComponent<Interactable>().dialogue = true;
            GetComponent<Interactable>().text = d1;
            GetComponent<Interactable>().playSecondPrefab = true;

            GameObject second = GetComponent<Interactable>().prefab;
            GetComponent<Interactable>().prefab = dialog;
            GetComponent<Interactable>().prefab2 = second;
        }

        if (PlayerPrefs.HasKey("FinishedGame") && PlayerPrefs.GetInt("FinishedGame") == 1)
        {
            GetComponent<Interactable>().dialogue = true;
            GetComponent<Interactable>().text = d2;
            GetComponent<Interactable>().playSecondPrefab = true;

            GameObject second = GetComponent<Interactable>().prefab;
            GetComponent<Interactable>().prefab = dialog;
            GetComponent<Interactable>().prefab2 = second;
        }
    }

}
