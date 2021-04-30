using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class PlayerController : MonoBehaviour
{
    public TankData tData;
    public TankMotor tMotor;
    private TankShooter tShooter;
    public enum ControlType {WASD, ArrowKeys };
    public ControlType input;



    // Start is called before the first frame update
    void Start()
    {
        tData = GetComponent<TankData>();
        tMotor = GetComponent<TankMotor>();
        tShooter = GetComponent<TankShooter>();


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToMove = Vector3.zero;

        switch (input)
		{
			case ControlType.WASD:
                //Handles Movement
                if (Input.GetKey(KeyCode.W))
                {
                    //Move Forward (+)
                    tMotor.Move(tData.moveSpeed);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    //Move Backward (-)
                    tMotor.Move(-tData.moveSpeed);

                }
                if (Input.GetKey(KeyCode.A))
                {
                    //Move Counterclockwise (-)
                    tMotor.Rotate(-tData.rotateSpeed);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    //Move Clockwise (+)
                    tMotor.Rotate(tData.rotateSpeed);
                }
                if (Input.GetKey(KeyCode.Space))
				{
                    tShooter.Shoot();
				}
                else
                {
                    tMotor.Move(0);
                }
                break;
                
            case ControlType.ArrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //Move Forward (+)
                    tMotor.Move(tData.moveSpeed);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //Move Backward (-)
                    tMotor.Move(-tData.moveSpeed);

                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //Move Counterclockwise (-)
                    tMotor.Rotate(-tData.rotateSpeed);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    //Move Clockwise (+)
                    tMotor.Rotate(tData.rotateSpeed);
                }
                else
                {
                    tMotor.Move(0);
                }
                break;



        }

    }
}
