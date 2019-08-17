using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip EnemyShatterSound;
    public AudioClip WeaponShatterSound;
    public AudioClip LightningSound;
    public AudioClip IceSound;
    public AudioClip MeteorSound;
    public AudioClip ImplosionSound;
    public AudioClip BowSound;

    public enum Sounds
    {
        EnemyShatter = 0, WeaponShatter = 1, Lightning = 2, Ice = 3, Meteor = 4, Implosion = 5, Bow = 6
    }

    public void PlaySound(Sounds sound, AudioSource audioSource)
    {
        switch (sound)
        {
            case Sounds.EnemyShatter:
                audioSource.clip = EnemyShatterSound;
                break;
            case Sounds.WeaponShatter:
                audioSource.clip = WeaponShatterSound;
                break;
            case Sounds.Lightning:
                audioSource.clip = LightningSound;
                break;
            case Sounds.Ice:
                audioSource.clip = IceSound;
                break;
            case Sounds.Meteor:
                audioSource.clip = MeteorSound;
                break;
            case Sounds.Implosion:
                audioSource.clip = ImplosionSound;
                break;
            case Sounds.Bow:
                audioSource.clip = BowSound;
                break;
        }
        audioSource.Play();
    }
}
