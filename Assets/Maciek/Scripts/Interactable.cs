using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : InteractFunc
{
    
    public string tekst;
    public override void GetText(string tekst)
    {
        base.GetText(tekst);
    }
    public override void MainFunction()
    {
        base.MainFunction();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Click");
    }

}