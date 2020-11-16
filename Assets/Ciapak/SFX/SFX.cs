using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{

    public AudioSource click_press;
    public AudioSource bullet_sound_1;
    public AudioSource damage_sound_1;
    public AudioSource enemy_hit_sound;
    public AudioSource mele_hit_sound;
    public AudioSource test_gun_sound;
    public AudioSource wall_hit_sound;


    public void PlayClickPress() {

        click_press.Play ();

    }

    public void PlayBulletSound1()
    {

        bullet_sound_1.Play();

    }

    public void PlayDamageSound1()
    {

        damage_sound_1.Play();

    }

    public void PlayEnemyHitSound()
    {

        enemy_hit_sound.Play();

    }

    public void PlayMeleHitSound()
    {

        mele_hit_sound.Play();

    }

    public void PlayTestGunSound()
    {

        test_gun_sound.Play();

    }

    public void PlayWallHitSound()
    {

        wall_hit_sound.Play();

    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
