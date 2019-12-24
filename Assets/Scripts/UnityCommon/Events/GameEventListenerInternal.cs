using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace UnityCommon.Events
{
	[System.Serializable]
	public class GameEventListenerInternal : IGameEventListener
	{

		public GameEvent listensTo;

		public UnityEvent response;

		public void OnEnable()
		{
			if (Application.isPlaying)
				listensTo.RegisterListener(this);
		}

		public void OnDisable()
		{
			if (Application.isPlaying)
				listensTo.UnregisterListener(this);
		}

		public void OnEventFired()
		{
			response?.Invoke();
		}

	}


	[System.Serializable]
	public class GameEventListenerInternal<T, TEvent> where TEvent : GameEvent<T, TEvent>
	{

		[System.Serializable]
		public class GeneratedType : GameEventListenerInternal<T, TEvent>
		{

		}

		[System.Serializable]
		public class GeneratedUnityEvent : UnityEvent<T>
		{

		}

		public TEvent listensTo;

		public GeneratedUnityEvent response;

		public void OnEnable(GameEventListener<T, TEvent> listener)
		{
			listensTo?.RegisterListener(listener);
		}

		public void OnDisable(GameEventListener<T, TEvent> listener)
		{
			listensTo?.UnregisterListener(listener);
		}

		public void OnEventFired(T arg)
		{
			response?.Invoke(arg);
		}

	}
}
