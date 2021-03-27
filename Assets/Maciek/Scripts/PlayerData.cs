using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData {

    public int cyberCoin;
    public PlayerData(Player player) {
        cyberCoin = player.cyberCoin;
    }
}
