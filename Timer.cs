/* A class which can be used as timer for other scripts.
 * Make a new instance to use this class.
 * Infinished!
 * Problem:
 *    1. MonoBehaviour can't be newed by keyword 'new', so how should
 *       it be instantiated in other scripts?
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool isTimerOver { get { return _isTimeOver; } }

    private bool _isTimeOver=false;
    public void StartTimer(float second)
    {
        StartCoroutine(TimerCountDown(second));
    }

    IEnumerator TimerCountDown(float second)
    {
        yield return new WaitForSeconds(second);
        _isTimeOver = true;
    }
}
