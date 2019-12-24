using System.Collections;
using System.Collections.Generic;
using UnityCommon.Events;
using UnityCommon.Variables;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
	[SerializeField] private GameEventListenerInternal onTargetTouchedGround;
	[SerializeField] private GameEventListenerInternal onScoreSystemActivated;

	[SerializeField] private IntReference score;

	[SerializeField] private GameEvent onScoreIncreasedEvent;


	private bool isScoreSystemActive = false; // To prevent increasing the score due to initial drop of cubes



	private void OnEnable()
	{
		onTargetTouchedGround.OnEnable();
		onScoreSystemActivated.OnEnable();
	}

	private void OnDisable()
	{
		onTargetTouchedGround.OnDisable();
		onScoreSystemActivated.OnDisable();
	}

	public void ActivateScoreSystem()
	{
		Debug.Log("Score System Activated");
		isScoreSystemActive = true;
	}

	public void OnTargetTouchedGround()
	{
		Debug.Log("On Target Touched Ground");

		if (!isScoreSystemActive)
			return;

		score.Value += 1;

		onScoreIncreasedEvent.Raise(this);

	}


}
