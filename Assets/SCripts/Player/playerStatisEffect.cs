using UnityEngine;

public class playerStatisEffect
{
    private static playermovement playermovement = playermovement.instance;

    public enum Effects
    {
        Slow,
        SpeedUp,
        Stun,
        Poison
    }

    public static void ApplyEffect(Effects effect)
    {
        switch (effect)
        {
            case Effects.Slow:
                SlowEffect(true);
                break;
            case Effects.SpeedUp:
                SpeedUpEffect(true);
                break;
            case Effects.Stun:
                StunEffect(true);
                break;
            case Effects.Poison:
                PoisEffect(true);
                break;
        }
    }

    public static void RemoveEffect(Effects effect)
    {
        switch (effect)
        {
            case Effects.Slow:
                SlowEffect(false);
                break;
            case Effects.SpeedUp:
                SpeedUpEffect(false);
                break;
            case Effects.Stun:
                StunEffect(false);
                break;
            case Effects.Poison:
                PoisEffect(false);
                break;
        }
    }

    static void SlowEffect(bool isActive)
    {
        if (isActive)
        {
            playermovement.speed = 3;
        }

        else
        {
            playermovement.speed = playermovement._originalSpeed;
        }
    }
    static void SpeedUpEffect(bool isActive)
    {
        if (isActive)
        {
            playermovement.speed *= 2; ;
        }

        else
        {
            playermovement.speed = playermovement._originalSpeed;
        }
    }
    static void StunEffect(bool isActive)
    {
        if (isActive)
        {
            playermovement.speed = 0;
        }

        else
        {
            playermovement.speed = playermovement._originalSpeed;
        }
    }

    static void PoisEffect(bool isActive)
    {
        if (isActive)
        {
            StatusEffectManager.StartPoisonEffect(0.3f, 1, playermovement);
        }
        else
        {
            StatusEffectManager.StopPoisonEffect(playermovement);
        }
    }

}
