using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Transform tf;
    private TankData data;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
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
