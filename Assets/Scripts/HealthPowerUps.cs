using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUps : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.healthPowerUps.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnDestroy()
	{
        if (GameManager.Instance == null)
        {
            return;
        }
        //remove from list if we are on the list
        if (GameManager.Instance.healthPowerUps.Contains(this.gameObject))
        {
            GameManager.Instance.healthPowerUps.Remove(this.gameObject);
        }
    }
}
