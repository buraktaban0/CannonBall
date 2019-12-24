using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Events;
using UnityCommon.Variables;
using UnityEngine;

namespace UnityCommon.UI.EventBinding
{
	[ExecuteInEditMode]
	public abstract class UIEventBinder<TElement, TReference> : GameEventListener where TElement : Behaviour where TReference : Reference
	{
		public TReference variable;

		public TElement element;


		public override void OnValidate()
		{
			base.OnValidate();

			if (element == null)
			{
				element = this.GetComponent<TElement>();
			}

		}


		public abstract override void OnEventFired();

	}
}
