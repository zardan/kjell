using Kjell.AvailableFunctions;
using PM;
using UnityEngine;

namespace Kjell
{
	public class KjellInitializer : MonoBehaviour
	{
		void Awake()
		{
			Main.RegisterFunction(new InputFunction());
			Main.RegisterFunction(new PrintFunction());
			Main.RegisterFunction(new RandintFunction());

			Main.RegisterCaseDefinitionContract<KjellCaseDefinition>();
		}
	}
}
