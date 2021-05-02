using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData {

    public int cyberCoin;
    public int[] uw;
    public int[] cw;
    public PlayerData(Player player) {
        if (player != null) {
            cyberCoin = player.cyberCoin;
            uw = player.unlockedWeapons;
            cw = player.currentWeapons;
        }
    }
}
