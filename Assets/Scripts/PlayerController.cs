using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class PlayerController : MonoBehaviour
{
    public TankData td;
    public enum ControlType {WASD, ArrowKeys };
    public ControlType input;



    // Start is called before the first frame update
    void Start()
    {
        td = GetComponent<TankData>();
        
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
                    td.mover.Move(transform.forward);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    //Move Backward (-)
                    td.mover.Move(-transform.forward);

                }
                if (Input.GetKey(KeyCode.A))
                {
                    //Move Counterclockwise (-)
                    td.mover.Rotate(false);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    //Move Clockwise (+)
                    td.mover.Rotate(true);
                }
                else
                {
                    td.mover.Move(directionToMove);
                }
                break;
                
            case ControlType.ArrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //Move Forward (+)
                    td.mover.Move(transform.forward);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //Move Backward (-)
                    td.mover.Move(-transform.forward);

                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //Move Counterclockwise (-)
                    td.mover.Rotate(false);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    //Move Clockwise (+)
                    td.mover.Rotate(true);
                }
                else
                {
                    td.mover.Move(directionToMove);
                }
                break;



        }

    }
}
