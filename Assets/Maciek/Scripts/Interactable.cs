using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : InteractFunc
{
    
    public override void MainFunction()
    {
        base.MainFunction();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Click");
    }

}