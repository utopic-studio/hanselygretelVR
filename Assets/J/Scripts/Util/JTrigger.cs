using UnityEngine;

namespace J
{

	[AddComponentMenu("J/Util/JTrigger")]
	[RequireComponent(typeof(Collider))]
	public class JTrigger : MonoBehaviour {

		[SerializeField]	string tagCondition = "";
		[SerializeField]	bool enterOnce = true;
		[SerializeField]	bool exitOnce = true;
		[SerializeField]	UnityEngine.Events.UnityEvent onEnter;
		[SerializeField]	UnityEngine.Events.UnityEvent onExit;
		[SerializeField]	UnityEngine.Events.UnityEvent onStay;

		protected Collider coll;
		protected bool m_enterEnabled = true, m_exitEnabled = true;

		void OnValidate() {
			tagCondition = tagCondition.Trim ();
		}

		void Start () {
			coll = GetComponent<Collider> ();
			coll.isTrigger = true;
		}

		void OnTriggerEnter(Collider other) {
			if (!isTagValid (other.tag))
				return;
			if (m_enterEnabled) {
				onEnter.Invoke ();
			}
			if (enterOnce)
				m_enterEnabled = false;
		}
		void OnTriggerExit(Collider other) {
			if (!isTagValid (other.tag))
				return;
			if (m_exitEnabled) {
				onExit.Invoke ();
			}
			if (exitOnce)
				m_exitEnabled = false;
		}
		void OnTriggerStay(Collider other) {
			if (!isTagValid (other.tag))
				return;
			onStay.Invoke ();
		}
		private bool isTagValid(string tag) {
			if (tagCondition == "")
				return true;
			return tag == tagCondition;
		}
	}

}