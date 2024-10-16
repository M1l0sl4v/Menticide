using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   [SerializeField] private AudioSource envirofxObject;
   [SerializeField] private AudioSource enemyfxObject;
   
   public static AudioManager instance;

   private void Awake()
   {
      instance = this;
   }

   public void environmentFX(AudioClip clip, Transform spawnPossition, float volume)
   {
        if (!DeathSequence.controlLock)
        {
            AudioSource audioSource = Instantiate(envirofxObject, spawnPossition.transform.position, quaternion.identity); // spawns gameobject to play the sound
            audioSource.clip = clip; //assigns the passed audioclip
            audioSource.volume = volume;
            audioSource.Play();
            float cliplength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clip.length);
        }
      
   }
   public void enemyFX(AudioClip clip, Transform spawnPossition, float volume)
   {
        if (!DeathSequence.controlLock)
        {
            AudioSource audioSource = Instantiate(enemyfxObject, spawnPossition.transform.position, quaternion.identity); // spawns gameobject to play the sound
            audioSource.clip = clip; //assigns the passed audioclip
            audioSource.volume = volume;
            audioSource.Play();
            float cliplength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clip.length);
        }
   }
}
