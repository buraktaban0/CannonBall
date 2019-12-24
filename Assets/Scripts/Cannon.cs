using System.Collections;
using System.Collections.Generic;
using UnityCommon.Events;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Cannon : MonoBehaviour
{

	[SerializeField] private GameObject cannonBallPrefab;

	[SerializeField] private LineRenderer lineRenderer;

	[SerializeField] private Vector2 aimOffset = Vector2.up;
	[SerializeField] private Vector2 aimRangeMultiplier = Vector2.one * 1.25f;

	[SerializeField] private float cannonRotationSpeed = 180f;

	[SerializeField] private float speedMultiplier = 10f;
	[SerializeField] private float horizontalWindMultiplier = 10f;

	[SerializeField] private GameEvent activateScoreSystemEvent;

	[SerializeField] [Range(0.1f, 1f)] private float trajectoryTimestep = 0.2f;


	private CannonBall currentBall;

	private Transform _t;

	private Transform camTransform;


	private Collider[] collidersBuffer = new Collider[20];

	private List<Vector3> trajectoryPoints = new List<Vector3>();


	private bool isReady = true;


	private bool isAiming = false;
	private bool isLaunched = false;


	private Vector3 initialVelocity = Vector3.zero;
	private Vector3 horizontalForce = Vector3.zero;



	private void OnValidate()
	{
		if (cannonBallPrefab == null)
			cannonBallPrefab = Resources.Load<GameObject>("CannonBall");

		lineRenderer = GetComponent<LineRenderer>();
	}


	void Awake()
	{
		_t = this.transform;
		camTransform = Camera.main.transform;
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			StartAiming();
		}



		if (isAiming && isReady)
		{
			var mousePos = Input.mousePosition;
			float aimX = (mousePos.x / Screen.width) - 0.5f + aimOffset.x;
			float aimY = (mousePos.y / Screen.height) - 0.5f + aimOffset.y;
			aimX *= aimRangeMultiplier.x;
			aimY *= aimRangeMultiplier.y;

			initialVelocity = (camTransform.forward + camTransform.right * aimX + camTransform.up * aimY) * speedMultiplier;
			horizontalForce = -camTransform.right * aimX * horizontalWindMultiplier;

			_t.rotation = Quaternion.RotateTowards(_t.rotation, Quaternion.LookRotation(initialVelocity, Vector3.up), Time.deltaTime * cannonRotationSpeed);

			UpdateTrajectory();

			if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				Fire();
			}

		}


	}


	private void OnDrawGizmos()
	{
		if (!isReady || !isAiming)
			return;

		var col = Gizmos.color;
		Gizmos.color = Color.cyan;

		foreach (var point in trajectoryPoints)
		{
			Gizmos.DrawWireSphere(point, 0.25f);
		}

		Gizmos.color = col;

	}


	private void UpdateTrajectory()
	{
		float time = 0f;

		Vector3 pos;

		trajectoryPoints.Clear();

		Vector3 totalForce = Physics.gravity + horizontalForce;

		while (time < 5f)
		{
			pos = _t.position + initialVelocity * time + totalForce * (time * time * 0.5f);

			trajectoryPoints.Add(pos);

			if (trajectoryPoints.Count > 1)
			{
				var previousPos = trajectoryPoints[trajectoryPoints.Count - 2];
				var ray = new Ray(previousPos, pos);
				var length = Vector3.Distance(previousPos, pos);
				if (Physics.Raycast(ray, length))
					break;
			}


			time += trajectoryTimestep;
		}

		lineRenderer.SetPositions(trajectoryPoints.ToArray());
		lineRenderer.positionCount = trajectoryPoints.Count;


	}


	private void StartAiming()
	{
		isAiming = true;
		lineRenderer.enabled = true;
	}


	private void Fire()
	{
		isAiming = false;

		lineRenderer.enabled = false;

		currentBall = GameObject.Instantiate(cannonBallPrefab, transform.position, transform.rotation).GetComponent<CannonBall>();
		currentBall.transform.localScale = transform.localScale;

		currentBall.SetTrajectory(initialVelocity, horizontalForce);

		isReady = false;

		activateScoreSystemEvent.Raise(this);

		StartCoroutine(Cooldown());
	}



	private IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(1f);
		isReady = true;
	}

}
