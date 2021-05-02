using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senses : MonoBehaviour
{

    public float hearingDistance = 3f;
    public float fieldOfView = 45f;// half of actual field of view
    public float viewDistance = 5; //how far away the ai can see the player


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanSee(GameObject target)
	{

        if(target == null)
		{
            return false;
		}
        // check field of view
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(vectorToTarget, transform.forward);

        if(angleToTarget <= fieldOfView)
		{
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) <= (viewDistance * viewDistance))
			{
                RaycastHit hit;

                if (Physics.Raycast(transform.position, vectorToTarget, out hit))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        return true;
                    }
                }
            }
            
		}
        return false;
	}
    public bool CanHear(GameObject target)
	{
		if (target == null)
		{
            return false;
		}
        return (Vector3.SqrMagnitude(transform.position - target.transform.position) <= (hearingDistance * hearingDistance));
	}
}
