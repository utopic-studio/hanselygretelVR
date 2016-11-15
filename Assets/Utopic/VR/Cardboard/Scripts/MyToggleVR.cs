using UnityEngine;

namespace Utopic.VR.UtopicCardboard {


[DisallowMultipleComponent]
public class MyToggleVR : MonoBehaviour {

	[HeaderAttribute("Use this checkbox or 'V' to toggle Cardboard VR mode")]
	public bool isVREnabled = true;

	private Cardboard m_cardboardComponent;

	void Start () {
		m_cardboardComponent = GameObject.FindObjectOfType<Cardboard> ();
		m_cardboardComponent.VRModeEnabled = isVREnabled;
	}

	// Update is called once per frame
	void Update () {
		if (Application.isEditor) {
			if (Input.GetKeyDown (KeyCode.V) || Input.GetKeyDown (KeyCode.JoystickButton9)) {

				m_cardboardComponent.VRModeEnabled = !m_cardboardComponent.VRModeEnabled;
				isVREnabled = m_cardboardComponent.VRModeEnabled;
				Debug.Log("Changed VRSettings.enabled to:"+m_cardboardComponent.VRModeEnabled);
			}
		} else { //android de pedro
			if (Input.GetKeyDown (KeyCode.JoystickButton7)) {

				m_cardboardComponent.VRModeEnabled = !m_cardboardComponent.VRModeEnabled;
				isVREnabled = m_cardboardComponent.VRModeEnabled;
			}

		}
	}
	void OnValidate() {
		if (m_cardboardComponent && m_cardboardComponent.VRModeEnabled != isVREnabled)
			m_cardboardComponent.VRModeEnabled = isVREnabled;
	}
}


}//END_NAMESPACE