using UnityEngine;
using System;
using System.Collections;

public static class CoroutineUtils {

    /**
     * Usage: StartCoroutine(CoroutineUtils.DelaySeconds(action, delay))
     * For example:
     *     StartCoroutine(CoroutineUtils.DelaySeconds(
     *         () => DebugUtils.Log("2 seconds past"),
     *         2);
     */
    public static IEnumerator DelaySeconds(Action action, float delay, bool ignoreTimeScale = false) {
        if (ignoreTimeScale)
        {
            yield return new WaitForSecondsRealtime(delay);
        }
        else
        {
            yield return new WaitForSeconds(delay);
        }
        action();
    }

    public static IEnumerator DelaySeconds(Action p, object recoveryDelayAfterHitted)
    {
        throw new NotImplementedException();
    }
}