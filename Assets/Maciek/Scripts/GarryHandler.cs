using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarryHandler : MonoBehaviour
{
    public GameObject secondOption;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Garry")) {
            PlayerPrefs.SetInt("Garry", 1);
        }
        else if (PlayerPrefs.GetInt("Garry") == 1) {
            GetComponent<Interactable>().dialogue = false;
            GetComponent<Interactable>().playSecondPrefab = false;
            GetComponent<Interactable>().prefab = secondOption;
        }
    }

}
