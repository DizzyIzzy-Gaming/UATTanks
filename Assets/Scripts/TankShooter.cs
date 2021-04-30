using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankData))]

public class TankShooter : MonoBehaviour
{
    private TankData tData;
    public GameObject firePoint; //
    public GameObject cannonBallPrefab;
    private bool canShoot = true;
    //private bool canShoot;
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
        if (canShoot)
		{
            //instantiate cannon ball.
            GameObject firedCannonBall = Instantiate(cannonBallPrefab, firePoint.transform.position, firePoint.transform.rotation);
            CannonBallData cannonBall = firedCannonBall.GetComponent<CannonBallData>();

            //Shoot forward rigidbody.addforce()
            Rigidbody cannonBallRB = firedCannonBall.GetComponent<Rigidbody>();
            cannonBallRB.AddForce(firePoint.transform.forward * tData.cannonBallSpeed);

            //Cannon ball needs data: Who fired it and how much will it do
            cannonBall.attacker = this.gameObject;
            cannonBall.attackDamage = tData.shootingDamage;
            canShoot = false;
            StartCoroutine(ShootRate());

        }
		else
		{
            Debug.Log("Can't shoot");
		}
        


	}

    IEnumerator ShootRate()
	{
        yield return new WaitForSeconds(tData.rateOfFire);
        canShoot = true;

	}
	
}
