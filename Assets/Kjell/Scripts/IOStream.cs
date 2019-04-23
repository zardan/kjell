using System.Collections;
using System.Collections.Generic;
using PM;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kjell
{
	public class IOStream : MonoBehaviour, IPMCompilerStarted, IPMLevelChanged, IPMCompilerStopped, IPMCaseSwitched,
		IPMSwitchedToSandbox
	{
		public static IOStream instance;

		[FormerlySerializedAs("InputLabelPlain")]
		public Sprite inputLabelPlain;

		[FormerlySerializedAs("InputLabelPop")]
		public Sprite inputLabelPop;

		[FormerlySerializedAs("InputValuePlain")]
		public Sprite inputValuePlain;

		[FormerlySerializedAs("InputValuePop")]
		public Sprite inputValuePop;

		[FormerlySerializedAs("LabelPrefab")]
		public GameObject labelPrefab;

		[FormerlySerializedAs("LatestReadInput")]
		public string latestReadInput;

		[FormerlySerializedAs("LinesWithInput")]
		public Dictionary<int, string> linesWithInput;

		[FormerlySerializedAs("PrintPrefab")]
		public GameObject printPrefab;

		[FormerlySerializedAs("ValuePrefab")]
		public GameObject valuePrefab;

		GameObject labelObject;
		GameObject valueObject;

		public void OnPMCaseSwitched(int caseNumber)
		{
			DeactivateLastInput();
			Clear();
		}

		public void OnPMCompilerStarted()
		{
			Clear();
		}

		public void OnPMCompilerStopped(StopStatus status)
		{
			DeactivateLastInput();
			StopAllCoroutines();
		}

		public void OnPMLevelChanged()
		{
			DeactivateLastInput();
			Clear();
		}

		public void OnPMSwitchedToSandbox()
		{
			Clear();
		}

		public void Print(string message)
		{
			CaseCorrection.NextOutput(message);

			GameObject outputObject = Instantiate(printPrefab, gameObject.transform, false);
			Output output = outputObject.GetComponent<Output>();
			message = message.Replace("\\n", "\n");
			output.text.text = message;
			outputObject.GetComponent<Container>().SetWidth(message.Length);
		}

		public IEnumerator TriggerInput(string message)
		{
			labelObject = Instantiate(labelPrefab, gameObject.transform, false);
			labelObject.GetComponent<InputLabel>().text.text = message;
			labelObject.GetComponent<InputLabel>().bubbleImage.sprite = inputLabelPop;
			labelObject.GetComponent<Container>().SetWidth(message.Length);

			yield return new WaitForSeconds(2 * (1 - PMWrapper.speedMultiplier));

			valueObject = Instantiate(valuePrefab, gameObject.transform, false);
			valueObject.GetComponent<InputValue>().bubbleImage.sprite = inputValuePop;
			valueObject.GetComponent<InputValue>().inputFieldBase.GetComponent<InputField>().Select();

			StartCoroutine(CaseCorrection.NextInput(valueObject));
		}

		public void InputSubmitted(string submittedText)
		{
			latestReadInput = submittedText;

			if (labelObject != null)
			{
				labelObject.GetComponent<InputLabel>().bubbleImage.sprite = inputLabelPlain;
			}

			valueObject.GetComponent<InputValue>().bubbleImage.sprite = inputValuePlain;

			PMWrapper.ResolveYield(submittedText);
		}

		void Start()
		{
			if (instance == null)
			{
				instance = this;
			}
		}

		void Clear()
		{
			foreach (Transform child in transform)
			{
				Destroy(child.gameObject);
			}
		}

		void DeactivateLastInput()
		{
			if (gameObject.transform.childCount > 0)
			{
				InputValue inputValue = gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject
					.GetComponent<InputValue>();
				if (inputValue != null)
				{
					inputValue.DeactivateInputValue();
				}
			}
		}
	}
}
