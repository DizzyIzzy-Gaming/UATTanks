using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(CharacterController))]

public class TankMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Transform tf;
    private TankData tData;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
        tData = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float speed)
    {
        //Vector that holds speed data
        Vector3 speedVector = tf.forward * speed;

        characterController.SimpleMove(speedVector);
        //transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, transform.rotation.y, 0));
    }

    public void Rotate(float speed)// Handles rotation for the tank
    {
        Vector3 rotateVector = Vector3.up * speed;

        tf.Rotate(rotateVector, Space.Self);
    }

    public bool RotateTowards(Vector3 target, float rotateSpeed)//Handles rotation for the AI
	{
        Vector3 adjustedTarget = new Vector3(target.x, transform.position.y, target.z);
        Vector3 vectorToTarget = adjustedTarget - transform.position;
        //Vector3 vectorToTarget = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);

        if (targetRotation == transform.rotation)//if the target rotation is the same as the direction we are facing do nothing
		{
            return false;
		}

        //Handles rotation and keeps the tank from rotating on the wrong axis
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        return false;
	}
}
