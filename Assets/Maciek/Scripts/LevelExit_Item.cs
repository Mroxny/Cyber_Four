using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit_Item : MonoBehaviour
{



    void Start()
    {
        //GameObject.Find("SceneMenager").GetComponent<Bulid_Script>().
    }

    public void EndLevel() {
        GameObject.Find("SceneMenager").GetComponent<LevelLoader>().LoadLevel(1);
    }
}
