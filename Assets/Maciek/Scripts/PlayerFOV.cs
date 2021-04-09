using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    [HideInInspector]
    public bool inRange = false;
    [HideInInspector]
    public Interactable interakcja = null;
    [HideInInspector]
    public WeaponInteract weapon;
    [HideInInspector]
    private Player player;

    public void DoInteract() {
        if (inRange)
        {
            if (interakcja != null)
            {
                interakcja.MainFunction();
            }
            else { 
                player = GameObject.FindObjectOfType<Player>();
                weapon.InPlayerHands = true;
                weapon.transform.SetParent(player.transform);
                weapon.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.3f);
                weapon.inHand = true;
            }
        }
    }

}
