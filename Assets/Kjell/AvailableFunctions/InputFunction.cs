using Compiler;
using Kjell;

public class InputFunction : Function
{

	public InputFunction()
	{
		name = "input";
		buttonText = "input()";
		pauseWalker = true;
		hasReturnVariable = false;
		inputParameterAmount.Add(0);
		inputParameterAmount.Add(1);
	}

	public override Variable runFunction(Scope currentScope, Variable[] inputParas, int lineNumber)
	{
		var inputLabel = "";

		if (inputParas.Length > 0)
		{
			if (inputParas[0].variableType == VariableTypes.number)
				inputLabel = inputParas[0].getNumber().ToString();
			else if (inputParas[0].variableType == VariableTypes.boolean)
				inputLabel = inputParas[0].getBool().ToString();
			else
				inputLabel = inputParas[0].getString();
		}

		IOStream.Instance.StartCoroutine(IOStream.Instance.TriggerInput(inputLabel));

		return new Variable("inputValue", "");
	}
}
