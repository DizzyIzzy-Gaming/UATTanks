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
        Vector3 speedVector = tf.forward * speed;

        characterController.SimpleMove(speedVector);
    }

    public void Rotate(float speed)
    {
        Vector3 rotateVector = Vector3.up * speed;

        tf.Rotate(rotateVector, Space.Self);
    }
}
