using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthController : MonoBehaviour
{

    public string pickUpSoundName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Player player = collision.GetComponent<Player>();
            if (player.HealPlayer(1)) {
                Destroy(gameObject);
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play(pickUpSoundName);
            }
            

        }
    }
}
