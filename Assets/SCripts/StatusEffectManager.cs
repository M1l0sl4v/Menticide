using System.Collections;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{

    private static Coroutine poisonCoroutine;
    public static void StartPoisonEffect(float damageTickRate, int damageAmount, MonoBehaviour caller)
    {
        if (poisonCoroutine == null)
        {
            poisonCoroutine = caller.StartCoroutine(DamageOverTime(damageTickRate, damageAmount));
        }
    }

    public static void StopPoisonEffect(MonoBehaviour caller)
    {
        if (poisonCoroutine != null)
        {
            caller.StopCoroutine(poisonCoroutine);
            poisonCoroutine = null;
        }
    }

    private static IEnumerator DamageOverTime(float damageTickRate, int damageAmount)
    {
        while (true)
        {
            yield return new WaitForSeconds(damageTickRate);
            playermovement.instance.TakeDamage(damageAmount);
        }
    }
}
