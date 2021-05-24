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
                if (player.GetComponent<Player>().GetCurrentGunSlot() != 0) {
                    player.GetComponent<Player>().SetWeaponInSlot(weapon.gameObject, player.GetComponent<Player>().GetCurrentGunSlot());
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("pickup_1");
                }
                else {
                    Debug.LogError("No Gun slots found");
                }
                    

            }
        }
    }

}
