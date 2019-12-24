using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.ResourceManagement;
using UnityEngine;

public class CannonBallFactory : PoolObjectFactory<CannonBall>
{
	public string ResourcePath { get; }

	public CannonBallFactory(string resourcePath)
	{
		ResourcePath = resourcePath;
	}

	public override CannonBall Produce()
	{
		return GameObject.Instantiate(Resources.Load<GameObject>(ResourcePath)).GetComponent<CannonBall>();
	}
}


