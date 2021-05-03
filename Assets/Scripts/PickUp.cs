using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

	public PowerUp powerup;
	public AudioClip soundEffect;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		PowerUpController powerUpController = other.gameObject.GetComponent<PowerUpController>();

		if (powerUpController != null)
		{
			//Add the power up we picked up to the power up controller
			powerUpController.Add(powerup);

			//PLay sound effect if one exists.WIP: still working on implementing sound
			//if(soundEffect != null)
			//{
			   //creates a transform where for the audioclip to attach to after the object has been destroyed
				//AudioSource.PlayClipAtPoint(soundEffect, transform.position);
			//}

			//Destroys gameobject because it has been collected
			Destroy(this.gameObject);
		}
	}
}
