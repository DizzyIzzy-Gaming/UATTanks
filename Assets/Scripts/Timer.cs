using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float timerDelay;
    private float timeTilNextEvent = 0f;
    private float nextEventTime;
    private float lastEventTime;

    // Start is called before the first frame update
    void Start()
    {
        lastEventTime = Time.time - timerDelay;
        nextEventTime = Time.time + timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastEventTime + timerDelay)
        {
            Debug.Log("timer 3 completed");
            lastEventTime = Time.time;
        }

        ///Second timer can tell the variable when is the next time an event can happen rather than counting down
        ///
        //if (Time.time >= nextEventTime)
        //{
        //    Debug.Log("timer 2 completed");
        //    nextEventTime = Time.time + timerDelay;
        //}

        ///first timer example very simple and easy
        ///
        //if (timeTilNextEvent > 0)
        //{
        //    timeTilNextEvent -= Time.deltaTime;
        //}
        //else
        //{
        //    Debug.Log("Timer1 has ended");
        //    timeTilNextEvent = timerDelay;
        //}
    }
}
