using System.Collections;
using System.Collections.Generic;
using UnityCommon.ResourceManagement;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CannonBall : MonoBehaviour, IPoolObject
{
	public Pool<CannonBall> Pool { get; set; }


	[SerializeField] private Rigidbody rb;



	private Vector3 horizontalForce = Vector3.zero;


	private void OnValidate()
	{
		rb = GetComponent<Rigidbody>();
	}


	private void OnEnable()
	{
		StartCoroutine(WaitAndPool());
	}

	IEnumerator WaitAndPool()
	{
		yield return new WaitForSeconds(4f);
		
		Pool.Return(this);
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

	public void OnPooled()
	{
		this.gameObject.SetActive(false);
	}

	public void OnRecycled()
	{
		this.gameObject.SetActive(true);
	}
}
