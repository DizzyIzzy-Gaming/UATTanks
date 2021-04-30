using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    [HideInInspector] public Transform tf;

    [Header("Is Controller Connected")]
    public bool useController = false;

    [HideInInspector] public bool isAlive = true;
    public bool canShoot = true;
    [HideInInspector] public bool isPaused = false;
    public bool isAi = false;

    private TankData td;
    //private TankShooter shoot;


    private CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        td = GetComponent<TankData>();
        //shoot = GetComponent<TankShooter
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 //   public void TurnTowards(Vector3 targetDirection)
	//{
 //       Quaternion targetRoation = Quaternion.LookRotation(targetDirection);

 //       tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, td.rotateSpeed * Time.deltaTime);
	//}

    public void Move (Vector3 direction)
    {
        cc.SimpleMove(direction * td.moveSpeed);
	}
    public void Rotate(bool isClockwise)
	{
        if (isClockwise)
		{
            transform.Rotate(new Vector3(0, td.rotateSpeed * Time.deltaTime, 0));
		}
        else
        {
            transform.Rotate(new Vector3(0, -td.rotateSpeed * Time.deltaTime, 0));
        }
    }
}
