using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    public static DebugTools instance;

    [Header("Player")]
    public bool playerInvincibility;
    public bool playerMoveSpeedOverride;
    public float overrideValue;
    public int damagePlayer;
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
}