using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CannonBall : MonoBehaviour
{


	[SerializeField] private Rigidbody rb;



	private Vector3 horizontalForce = Vector3.zero;


	private void OnValidate()
	{
		rb = GetComponent<Rigidbody>();
	}


	private void Awake()
	{
		Destroy(this.gameObject, 4f);
	}



	private void FixedUpdate()
	{

		rb.AddForce(horizontalForce, ForceMode.Acceleration);

	}

	public void SetTrajectory(Vector3 initialVelocity, Vector3 horizontalForce)
	{
		rb.velocity = initialVelocity;
		this.horizontalForce = horizontalForce;
	}

}
