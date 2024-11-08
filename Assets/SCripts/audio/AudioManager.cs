using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   [SerializeField] private AudioSource envirofxObject;
   [SerializeField] private AudioSource enemyfxObject;
   [SerializeField] private AudioSource playerfxObject;

   
   public static AudioManager instance;

   public GameObject audioEmpty;

   private void Awake()
   {
      instance = this;
   }

   public void environmentFX(AudioClip clip, Transform spawnPossition, float volume, float pitch)
   {
        if (!DeathSequence.controlLock)
        {
            AudioSource audioSource = Instantiate(envirofxObject, spawnPossition.position, quaternion.identity,audioEmpty.transform); // spawns gameobject to play the sound
            audioSource.clip = clip; //assigns the passed audioclip
            audioSource.volume = volume;
            audioSource.Play();
            audioSource.pitch = pitch;
            float cliplength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clip.length);
        }
      
   }
   public void enemyFX(AudioClip clip, Transform spawnPossition, float volume, float pitch)
   {
        if (!DeathSequence.controlLock)
        {
            AudioSource audioSource = Instantiate(enemyfxObject, spawnPossition.transform.position, quaternion.identity, audioEmpty.transform); // spawns gameobject to play the sound
            audioSource.clip = clip; //assigns the passed audioclip
            audioSource.volume = volume;
            audioSource.Play();
            audioSource.pitch = pitch;
            float cliplength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clip.length);
        }
   }
   public void playerFX(AudioClip clip, Transform spawnPossition, float volume, float pitch)
   {
       if (!DeathSequence.controlLock)
       {
           AudioSource audioSource = Instantiate(playerfxObject, spawnPossition.transform.position, quaternion.identity, audioEmpty.transform); // spawns gameobject to play the sound
           audioSource.clip = clip; //assigns the passed audioclip
           audioSource.volume = volume;
           audioSource.Play();
           audioSource.pitch = pitch;
           float cliplength = audioSource.clip.length;
           Destroy(audioSource.gameObject, clip.length);
       }
   }
}
