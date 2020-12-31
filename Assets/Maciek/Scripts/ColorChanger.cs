using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ColorChanger : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    private void Start() {
        GameObject sceneMenager=GameObject.FindWithTag("SceneMenager");
        
        if (SceneManager.GetActiveScene().buildIndex == 0) {                            //MainMenu
            MainMenu_SM Events = sceneMenager.GetComponent<MainMenu_SM>();
            Events.OnColorChange += Events_OnColorChange;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 4) {                        //MissionScene
            Bulid_Script Events = sceneMenager.GetComponent<Bulid_Script>();
            Events.OnColorChange += Events_OnColorChange;
        }
        Transform par = null;
        if (transform.parent != null) {
            par = transform.parent;
        }
        else {
            par = transform;
        }

            DetectAllChildren(par,targets);
        if (PlayerPrefs.HasKey("ColorId")) {
            Events_OnColorChange(PlayerPrefs.GetInt("ColorId"));
        }
        
    }
    public class OnSpacePressedEventArgs : EventArgs
    {
        public int ColorId;
    }
    private void DetectAllChildren(Transform parent, List<GameObject> list) {
        foreach (Transform child in parent) {
                list.Add(child.gameObject);
            DetectAllChildren(child, list);
        }
      
    }

    
    private void Events_OnColorChange(int e) {
        
        switch (e) {
            case 1:
                if (transform.parent != null) {
                    if (transform.parent.GetComponent<SpriteRenderer>() == true) {
                        transform.parent.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else if (transform.parent.GetComponent<Image>() == true) {
                        transform.parent.GetComponent<Image>().color = Color.green;
                    }
                    else if (transform.parent.GetComponent<TextMeshProUGUI>() == true) {
                        transform.parent.GetComponent<TextMeshProUGUI>().color = Color.green;
                    }
                }
                foreach (GameObject j in targets) {

                    if (j.GetComponent<SpriteRenderer>() == true) {
                        j.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else if (j.GetComponent<Image>() == true) {
                        j.GetComponent<Image>().color = Color.green;
                    }
                    else if (j.GetComponent<TextMeshProUGUI>() == true) {
                        j.GetComponent<TextMeshProUGUI>().color = Color.green;
                    }
                }
                break;
            case 2:
                if (transform.parent!=null) {
                    if (transform.parent.GetComponent<SpriteRenderer>() != null) {
                        transform.parent.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else if (transform.parent.GetComponent<Image>() != null) {
                        transform.parent.GetComponent<Image>().color = Color.red;
                    }
                    else if (transform.parent.GetComponent<TextMeshProUGUI>() != null) {
                        transform.parent.GetComponent<TextMeshProUGUI>().color = Color.red;
                    }
                }
                foreach (GameObject j in targets) {

                    if (j.GetComponent<SpriteRenderer>() == true) {
                        j.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else if (j.GetComponent<Image>() == true) {
                        j.GetComponent<Image>().color = Color.red;
                    }
                    else if (j.GetComponent<TextMeshProUGUI>() == true) {
                        j.GetComponent<TextMeshProUGUI>().color = Color.red;
                    }
                }
                break;
            case 3:
                if (transform.parent != null) {
                    if (transform.parent.GetComponent<SpriteRenderer>() == true) {
                        transform.parent.GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                    else if (transform.parent.GetComponent<Image>() == true) {
                        transform.parent.GetComponent<Image>().color = Color.yellow;
                    }
                    else if (transform.parent.GetComponent<TextMeshProUGUI>() == true) {
                        transform.parent.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                    }
                }
                foreach (GameObject j in targets) {

                    if (j.GetComponent<SpriteRenderer>() == true) {
                        j.GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                    else if (j.GetComponent<Image>() == true) {
                        j.GetComponent<Image>().color = Color.yellow;
                    }
                    else if (j.GetComponent<TextMeshProUGUI>() == true) {
                        j.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                    }
                }
                break;
            case 4:
                if (transform.parent != null) {
                    if (transform.parent.GetComponent<SpriteRenderer>() == true) {
                        transform.parent.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    else if (transform.parent.GetComponent<Image>() == true) {
                        transform.parent.GetComponent<Image>().color = Color.blue;
                    }
                    else if (transform.parent.GetComponent<TextMeshProUGUI>() == true) {
                        transform.parent.GetComponent<TextMeshProUGUI>().color = Color.blue;
                    }
                }
                foreach (GameObject j in targets) {

                    if (j.GetComponent<SpriteRenderer>() == true) {
                        j.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    else if (j.GetComponent<Image>() == true) {
                        j.GetComponent<Image>().color = Color.blue;
                    }
                    else if (j.GetComponent<TextMeshProUGUI>() == true) {
                        j.GetComponent<TextMeshProUGUI>().color = Color.blue;
                    }
                }
                break;
        }

    }
}