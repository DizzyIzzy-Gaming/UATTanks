using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankData))]

public class TankShooter : MonoBehaviour
{
    private TankData tData;
    public GameObject firePoint;
    public GameObject cannonBallPrefab;
    // Start is called before the first frame update
    void Start()
    {
        tData = GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Shoot()
	{
        //throw new NotImplementedException();
        //check if we can shoot

        //instantiate cannon ball.
        GameObject firedCannonBall = Instantiate(cannonBallPrefab);

        //Shoot forward rigidbody.addforce()

        //Cannon ball needs data: Who fired it and how much will it do
        CannonBallData cannonBall = firedCannonBall.GetComponent<CannonBallData>();
        cannonBall.attacker = this.gameObject;
        cannonBall.attackDamage = tData.shootingDamage;


	}

	
}
