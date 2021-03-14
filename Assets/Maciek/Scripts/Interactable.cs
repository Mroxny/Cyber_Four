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
        throw new System.NotImplementedException();

    }

    public void DoFunc() {
        GetText(tekst);
        MainFunction();
    }
}
