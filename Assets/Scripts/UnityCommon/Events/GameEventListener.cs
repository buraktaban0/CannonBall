using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace UnityCommon.Events
{
	public class GameEventListener : MonoBehaviour
	{
		public GameEventListenerInternal gameEvent;


		private void Awake()
		{
			//gameEvent?.response.AddListener(OnEventFired);
		}

		private void OnEnable()
		{
			if(Application.isPlaying)
			gameEvent.OnEnable();
		}
		private void OnDisable()
		{
				gameEvent.OnDisable();
		}

		public virtual void OnValidate()
		{
			if (Application.isPlaying == true)
			{
				gameEvent?.OnEnable();
			}
		}
		

		public virtual void OnEventFired()
		{
			//gameEvent?.OnEventFired();
		}

	}


	public abstract class GameEventListener<T, TEvent> : MonoBehaviour where TEvent : GameEvent<T, TEvent>
	{


		public GameEventListenerInternal<T, TEvent>.GeneratedType gameEvent;



		public virtual void OnEnable()
		{
			if (Application.isPlaying == true)
				gameEvent?.OnEnable(this);
		}

		public virtual void OnDisable()
		{
			if (Application.isPlaying == true)
				gameEvent?.OnDisable(this);
		}

		public virtual void OnEventFired(T arg)
		{
			gameEvent?.OnEventFired(arg);
		}

	}

}
