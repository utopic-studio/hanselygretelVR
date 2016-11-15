using UnityEngine;
using System.Collections;

public class AnswerButton : MonoBehaviour {

	[HeaderAttribute("set this to 1 (A), 2 (B) or 3 (C)")]
	public int indexNumberOfAnswer = -1;

	private int questionNumber;
	private Question questionInParent;

	void Start() {
		questionInParent = GetComponentInParent<Question> ();
	}

	public void DoWhenClicked() {
		questionNumber = GetComponentInParent<Question> ().questionNumber;
		QuestionManager.Instance.addAnswer (questionNumber, indexNumberOfAnswer);
		if (QuestionManager.Instance.isQuestionCorrectlyAnswered (questionNumber)) {
			HideQuestion ();
			questionInParent.onFinishAnswering.Invoke ();
			questionInParent.correctSound.Play ();
		}
		else {
			if (!QuestionManager.Instance.isQuestionWithTriesLeft (questionNumber)) {
				HideQuestion ();
				questionInParent.onFinishAnswering.Invoke ();
			}
			questionInParent.incorrectSound.Play ();
		}
			
			
	}

	public void HideQuestion() {
		questionInParent.Hide ();
	}
}
