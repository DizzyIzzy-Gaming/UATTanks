using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public float timeToWait;
    private float timeRemaining;
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining <= 0f)
		{
            Debug.Log("The Timer has Ended");
            ResetTimer();
		}
    }

    void ResetTimer()
	{
        timeRemaining = timeToWait;
	}
}
