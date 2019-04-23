using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kjell
{
	public class InputValue : MonoBehaviour
	{
		[FormerlySerializedAs("BubbleImage")]
		public Image bubbleImage;

		[FormerlySerializedAs("InputField")]
		public InputField inputField;

		[FormerlySerializedAs("InputFieldBase")]
		public GameObject inputFieldBase;

		[FormerlySerializedAs("SendButton")]
		public GameObject sendButton;

		[FormerlySerializedAs("SubmittedText")]
		public Text submittedText;

		bool hasBeenSubmitted;

		public void SubmitInput()
		{
			hasBeenSubmitted = true;

			if (!string.IsNullOrEmpty(inputField.text))
			{
				submittedText.text = inputField.text;
			}

			inputFieldBase.SetActive(false);
			sendButton.SetActive(false);
			submittedText.gameObject.SetActive(true);

			GetComponent<Container>().SetWidth(inputField.text.Length);

			IOStream.instance.InputSubmitted(submittedText.text);
		}

		public IEnumerator StartInputAnimation(string message)
		{
			foreach (char character in message)
			{
				inputField.text += character;
				inputField.caretPosition = message.Length;

				yield return new WaitForSeconds((1 - PMWrapper.speedMultiplier) * 0.4f);
			}

			yield return new WaitForSeconds((1 - PMWrapper.speedMultiplier) * 2);

			SubmitInput();
		}

		public void DeactivateInputValue()
		{
			if (!string.IsNullOrEmpty(inputField.text))
			{
				submittedText.text = inputField.text;
			}

			inputFieldBase.SetActive(false);
			sendButton.SetActive(false);
			submittedText.gameObject.SetActive(true);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
			{
				if (!hasBeenSubmitted)
				{
					SubmitInput();
				}
			}
		}
	}
}
