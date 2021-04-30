using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class PlayerController : MonoBehaviour
{
    public TankData tData;
    public TankMover tMover;
    public TankShooter tShooter;
    public enum ControlType {WASD, ArrowKeys };
    public ControlType input;



    // Start is called before the first frame update
    void Start()
    {
        tData = GetComponent<TankData>();
        tMover = GetComponent<TankMover>();
        tShooter = GetComponent<TankShooter>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToMove = Vector3.zero;

        switch (input)
		{
			case ControlType.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    //Move Forward (+)
                    tData.mover.Move(transform.forward);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    //Move Backward (-)
                    tData.mover.Move(-transform.forward);

                }

                //Handles Rotation
                if (Input.GetKey(KeyCode.A))
                {
                    //Move Counterclockwise (-)
                    tData.mover.Rotate(false);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    //Move Clockwise (+)
                    tData.mover.Rotate(true);
                }
                else
                {
                    tData.mover.Move(directionToMove);
                }

                //Handles shooting
                if (Input.GetKey(KeyCode.Space))
				{
                    tShooter.Shoot();
				}
                break;
                
            case ControlType.ArrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //Move Forward (+)
                    tData.mover.Move(transform.forward);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //Move Backward (-)
                    tData.mover.Move(-transform.forward);

                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //Move Counterclockwise (-)
                    tData.mover.Rotate(false);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    //Move Clockwise (+)
                    tData.mover.Rotate(true);
                }
                else
                {
                    tData.mover.Move(directionToMove);
                }
                break;
            default:
                Debug.LogError("[InputController]Input scheme not implemented");
                break;



        }

    }
}
