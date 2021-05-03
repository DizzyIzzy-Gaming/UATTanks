using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Room myRoom;
    // Start is called before the first frame update
    void Start()
    {
        myRoom = GetComponentInParent<Room>();
        GameManager.Instance.waypoints.Add(this.gameObject);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
