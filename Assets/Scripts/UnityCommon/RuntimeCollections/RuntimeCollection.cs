using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Events;
using UnityCommon.Singletons;
using UnityEngine;

namespace UnityCommon.RuntimeCollections
{
	public abstract class RuntimeCollection<T/*, TEvent*/> : ScriptableObject/* where TEvent : GameEvent<T, TEvent>*/
	{
		[SerializeField] private bool allowDuplicates = false;
		public bool AllowDuplicates => allowDuplicates;

		public GameEvent onModified;

		//public TEvent onItemAdded, onItemRemoved;


		public List<T> items = new List<T>();

		public int Count => items.Count;



		public T this[int i]
		{
			get => items[i];
			set => items[i] = value;
		}

		public void Add(T item)
		{
			if (allowDuplicates || !items.Contains(item))
			{
				items.Add(item);

				/*onModified?.Raise(this);
				onItemAdded?.Raise(this, item);*/

			}
		}

		public void Remove(T item)
		{
			if (items.Contains(item))
			{
				items.Remove(item);

				/*onModified?.Raise(this);
				onItemRemoved?.Raise(this, item);*/

			}
		}

	}
}
