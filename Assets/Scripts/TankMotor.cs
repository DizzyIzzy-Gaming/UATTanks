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

    public bool RotateTowards(Transform targetTransform)
    {
        //rotate towards that object
        //find vector to target
        Vector3 targetVector = targetTransform.position - transform.position;
        //find direction to look towards
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);
        //find rotation that is partway to that rotation
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, td.rotateSpeed);
        //change to that new rotation
        transform.rotation = newRotation;

    }
}
