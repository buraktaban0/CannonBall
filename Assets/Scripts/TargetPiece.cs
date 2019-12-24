using System.Collections;
using System.Collections.Generic;
using UnityCommon.Events;
using UnityEngine;

public class TargetPiece : MonoBehaviour
{

	[SerializeField] private GameEvent onTouchedGround;

	[SerializeField] private bool touchedGround = false;

	private void OnCollisionEnter(Collision collision)
	{
		if (touchedGround || collision.gameObject.layer != 8)
			return;

		touchedGround = true;

		onTouchedGround.Raise(this);


	}


}
