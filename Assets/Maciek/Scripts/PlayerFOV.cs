using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    [HideInInspector]
    public bool inRange = false;
    [HideInInspector]
    public Interactable interakcja;

    public void DoInteract() {
        Debug.Log(interakcja.name);
        if (inRange)
        {
            interakcja.MainFunction();
        }
    }

}
