using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantProjectile : MonoBehaviour
{

    [SerializeField] private Transform player;
	[SerializeField] private float viewRadius;
	[SerializeField] private float cdTime;

	[SerializeField] private bool checkHitTransform;
	[SerializeField] private float powerMultiplier;
	[SerializeField] private float power;
	[SerializeField] private Transform storeProjectile;

	[SerializeField] private Rigidbody2D plantProjectile;
	[SerializeField] private GameObject projectilePrefab;

	[SerializeField] private AttachToProjectile attachToProjectile;

	private SpriteRenderer plantVisible;

	private float viewAngle = 360;

	private float dstToTarget;

	private bool foundPlayer;
	private float cooldown;
	private float roundUp;


    // Start is called before the first frame update
    void Start()
    {
		
		//plantVisible = GetComponent<SpriteRenderer>();

		foundPlayer = false;
		cooldown = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPlayerDist();
		AttackPlayer();

        // if condition that player is in radius
    }


    private void CheckPlayerDist()
    {
		Transform target = player;
		Vector3 dirToTarget = (target.position - storeProjectile.position).normalized;
		dstToTarget = Vector2.Distance(storeProjectile.position, target.position);

		if (Vector3.Angle(storeProjectile.right, dirToTarget) < viewAngle / 2 && dstToTarget <= (viewRadius + 15))
		{

			RaycastHit2D findingPlayer = Physics2D.Raycast(transform.position, dirToTarget, viewRadius);

			if (findingPlayer.collider == true && findingPlayer.collider.CompareTag("Player"))
			{
				foundPlayer = true;
			}
			else
			{
				foundPlayer = false;
				cooldown = 0;
			}

			if (checkHitTransform)
			{
				Debug.Log("Light Hit: " + findingPlayer.transform.name);
			}
		}
	}


	private void AttackPlayer()
    {
        if (foundPlayer)
        {
			cooldown += Time.deltaTime;
			if(cooldown >= cdTime)
            {
				//ReadyProjectile();
				GameObject orb = Instantiate(projectilePrefab) as GameObject;
				orb.transform.position = storeProjectile.position;
				dstToTarget = Vector2.Distance(storeProjectile.position, player.position);
				plantProjectile = orb.GetComponent<Rigidbody2D>();
				//plantProjectile.velocity = transform.right * powerMultiplier * dstToTarget * power;
				plantProjectile.velocity = new Vector2(power * powerMultiplier * dstToTarget, 0);
				cooldown = 0;
            }
        }
    }


	private void ReadyProjectile()
    {
		GameObject orb = Instantiate(projectilePrefab) as GameObject;
		orb.transform.position = storeProjectile.position;
	}
}



