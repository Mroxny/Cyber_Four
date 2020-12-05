using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInfo : MonoBehaviour
{
    [Header("Settings")]
    [Space(10)]
    public string platformKey="";
    public bool withEnemies=false;
    GameObject Exits;


    public void OpenExits() {
        Exits = transform.GetChild(0).gameObject;
        if (platformKey.Contains("t")) {
            Exits.transform.GetChild(0).transform.gameObject.SetActive(false);
        }
        if (platformKey.Contains("r")) {
            Exits.transform.GetChild(1).transform.gameObject.SetActive(false);
        }
        if (platformKey.Contains("b")) {
            Exits.transform.GetChild(2).transform.gameObject.SetActive(false);
        }
        if (platformKey.Contains("l")) {
            Exits.transform.GetChild(3).transform.gameObject.SetActive(false);
        }
    }

}
