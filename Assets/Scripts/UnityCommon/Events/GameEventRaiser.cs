using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Variables;
using UnityEngine;

namespace UnityCommon.Events
{
	public class GameEventRaiser : MonoBehaviour
	{

		public GameEvent gameEvent;

		public FloatReference delay;

		public void Raise()
		{
			if (delay.Value <= 0.0f)
			{
				RaiseNoDelay();
				return;
			}
			

		}


		public void RaiseNoDelay()
		{
			gameEvent.Raise(this);
		}


		private IEnumerator WaitAndRaise()
		{
			if (delay.Value <= 0.0f)
			{
				RaiseNoDelay();
				yield break;
			}

			yield return new WaitForSecondsRealtime(delay.Value);

			RaiseNoDelay();

		}


	}
}
