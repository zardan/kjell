using System;
using System.Collections;
using System.Collections.Generic;
using Kjell;
using PM;
using UnityEngine;

public class CaseCorrection : MonoBehaviour, IPMCaseSwitched, IPMTimeToCorrectCase, IPMCompilerStarted
{
	public static bool hasTestDefined;

	private static int inputIndex;
	private static int outputIndex;

	private static List<string> inputs;
	private static List<string> outputs;

	private static string errorMessage;

	public static IEnumerator NextInput(GameObject inputValueObject)
	{
		yield return new WaitForSeconds((1 - PMWrapper.speedMultiplier) * 2);

		if (hasTestDefined && PMWrapper.LevelMode == LevelMode.Case)
		{
			if (inputs == null || inputs.Count == 0)
				PMWrapper.RaiseTaskError("Jag förväntade mig inga inmatningar, så nu vet jag inte vad jag ska mata in.");

			else if (inputIndex > inputs.Count - 1)
				PMWrapper.RaiseTaskError("För många inmatningar. Jag förväntade mig bara " + inputs.Count + " inmatningar.");

			else
			{
				var nextInput = inputs[inputIndex];
				IOStream.Instance.StartCoroutine(inputValueObject.GetComponent<InputValue>().StartInputAnimation(nextInput));
				inputIndex++;
			}
		}
	}

	public static void NextOutput(string output)
	{
		if (hasTestDefined)
		{
			if (outputIndex > outputs.Count - 1)
				errorMessage = "För många utskrifter. Jag förväntade mig bara " + outputs.Count + " utskrifter.";

			else if (output != outputs[outputIndex] && string.IsNullOrEmpty(errorMessage))
				errorMessage = "Fel i den " + StringifyNumber(outputIndex + 1) + " utskriften. Programmet skrev ut <b>" + output + "</b> men jag förväntade mig <b>" + outputs[outputIndex] + "</b>.";

			outputIndex++;
		}
	}

	private static string StringifyNumber(int number)
	{
		var First20 = new List<string>()
		{
			"första",
			"andra",
			"tredje",
			"fjärde",
			"femte",
			"sjätte",
			"sjunde",
			"åttonde",
			"nionde",
			"tionde",
			"elfte",
			"tolfte",
			"trettonde",
			"fjortonde",
			"femtonde",
			"sextonde",
			"sjuttonde",
			"artonde",
			"nittonde"
		};
		var tensStart = new List<string>()
		{
			"tjugo",
			"trettio",
			"fyrtio",
			"femtio",
			"sextio",
			"sjuttio",
			"åttio",
			"nittio"
		};
		var tensEnd = "nde";

		if (number < 1)
			throw new ArgumentOutOfRangeException("number", "Argument must be greater than 0");

		if (number < 20)
			return First20[number - 1];
		if (number < 100)
		{
			if (number % 10 == 0)
				return tensStart[number / 10 - 2] + tensEnd;
			else
				return tensStart[number / 10 - 2] + First20[number % 10 - 1];
		}
		else
			return number.ToString();
	}

	private static void CheckTooFewInputs()
	{
		if (inputs != null && inputIndex < inputs.Count)
			errorMessage = "För få inmatningar. Jag förväntade mig " + inputs.Count + " inmatningar.";
		else if (outputs != null && outputIndex < outputs.Count)
			errorMessage = "För få utskrifter. Jag förväntade mig " + outputs.Count + " utskrifter.";
	}

	public void OnPMCaseSwitched(int caseNumber)
	{
		if (PMWrapper.LevelData.cases != null &&
			PMWrapper.LevelData.cases.Count > 0 && 
		    PMWrapper.LevelData.cases[caseNumber].caseDefinition != null && 
		    PMWrapper.LevelData.cases[caseNumber].caseDefinition.test != null)
		{
			hasTestDefined = true;

			inputIndex = 0;
			inputs = PMWrapper.LevelData.cases[caseNumber].caseDefinition.test.input;

			outputIndex = 0;
			outputs = PMWrapper.LevelData.cases[caseNumber].caseDefinition.test.output;

			if (outputs == null || outputs.Count == 0)
				throw new Exception("There is a test defined but no output defined.");
		}
		else
		{
			hasTestDefined = false;
			inputs = null;
			outputs = null;
			inputIndex = 0;
			outputIndex = 0;
		}
	}

	public void OnPMTimeToCorrectCase()
	{
		CheckTooFewInputs();

		if (!string.IsNullOrEmpty(errorMessage))
			PMWrapper.RaiseTaskError(errorMessage);
		else
			PMWrapper.SetCaseCompleted();
	}

	public void OnPMCompilerStarted()
	{
		errorMessage = "";
	}
}
