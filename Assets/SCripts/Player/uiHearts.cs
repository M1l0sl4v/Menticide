using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHearts : MonoBehaviour
{
    public GameObject heartPrefab;
    public List<GameObject> hearts = new List<GameObject>();
    [SerializeField] private AudioClip takeDamageSound;
    [SerializeField] private AudioClip deathSound;


    public void StartHealth(int initialHealth)
    {
        AddHeart(initialHealth);
    }
    public void AddHeart(int howMuch)
    {
        for (int i = 0; i < howMuch; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            hearts.Add(heart);
            Animator animator = heart.GetComponent<Animator>();
            if (animator != null)
            {
               // animator.SetTrigger("HeartAdd");
            }
        }
    }
    public void RemoveHeart(int howMuch)
    {
        for (int i = 0; i < howMuch; i++)
        {
            GameObject lastHeart = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Animator animator = lastHeart.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("HeartLost");
                Destroy(lastHeart, 0.4f);
            }
            else
            {
                Destroy(lastHeart);
            }
            if (hearts.Count < 1)
            {
                AudioManager.instance.playerFX(deathSound, transform, 1f, 1);
                DeathSequence.instance.StartDeathSequence();
                MusicManager.instance.pauseMusic();
                ambianceManager.instance.PlayAmbiance("End Ambiance");
            }

            if (hearts.Count >= 1)
            {
                AudioManager.instance.playerFX(takeDamageSound, transform, 1f, 1f);
            }
        }
    }
}