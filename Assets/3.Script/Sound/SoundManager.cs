using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Jump;
    public AudioClip GetItem;
    public AudioClip TakeDamge;
    public AudioClip DropItem;
    public AudioClip AttSound;
    public AudioClip SnailDie;



    public static SoundManager instance;

    private void Awake()
    {
        if(SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }


    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(Jump);
    }

    public void PlayGetItem()
    {
        audioSource.PlayOneShot(GetItem);
    }

    public void PlayTakeDamge()
    {
        audioSource.PlayOneShot(TakeDamge);
    }
    public void PlayDropItem()
    {
        audioSource.PlayOneShot(DropItem);
    }
    public void PlayAttSound()
    {
        audioSource.PlayOneShot(AttSound);
    }   
    public void PlaySnailDie()
    {
        audioSource.PlayOneShot(SnailDie);
    }
}
