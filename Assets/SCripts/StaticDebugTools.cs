using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDebugTools : MonoBehaviour
{
    public static StaticDebugTools instance;

    [Header("Player")]
    public bool playerInvincibility;
    public bool playerMoveSpeedOverride;
    public float overrideValue;
    public int damagePlayer;

    [Header("Game")]
    public bool pauseAfterEveryRow;
    public enum Algorithm
    {
        Cache,
        Picker
    }
    public Algorithm tileManagerAlgorithm;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoveSpeedOverride) { playermovement.instance.speed = overrideValue; }

        if (damagePlayer > 0)
        {
            playermovement.instance.TakeDamage(damagePlayer);
            damagePlayer = 0;
        }
    }

    public void ChangeAlgorithm(int algorithm)
    {
        tileManagerAlgorithm = (Algorithm) algorithm;
    }
}
