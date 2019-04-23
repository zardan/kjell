using System.Collections;
using System.Collections.Generic;
using Kjell.AvailableFunctions;
using PM;
using UnityEngine;

namespace Kjell
{
	public class KjellInitializer : MonoBehaviour
	{
		private void Awake()
		{
			Main.RegisterFunction(new InputFunction());
			Main.RegisterFunction(new PrintFunction());
			Main.RegisterFunction(new RandomIntFunction());

			Main.RegisterCaseDefinitionContract<KjellCaseDefinition>();
		}
	}
}
