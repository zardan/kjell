using Compiler;
using UnityEngine;

public class RandintFunction : Function
{
	public RandintFunction()
	{
		name = "randint";
		buttonText = "randint()";
		inputParameterAmount.Add(2);
		hasReturnVariable = true;
		pauseWalker = false;
	}

	public override Variable runFunction(Scope currentScope, Variable[] inputParas, int lineNumber)
	{
		foreach (var parameter in inputParas)
		{
			if (parameter.variableType != VariableTypes.number || parameter.getNumber() != (int)parameter.getNumber())
				PMWrapper.RaiseError(lineNumber, "Fel datatyp. Parametrarna till randint får bara vara heltal.");
		}

		var start = (int) inputParas[0].getNumber();
		var end = (int)inputParas[1].getNumber();

		if (start > end)
			PMWrapper.RaiseError("Fel i parametrarna till randint. Första parametern ska vara mindre än den andra");

		var randomNumber = Random.Range(start, end+1);

		return new Variable("random", randomNumber);
	}
}
