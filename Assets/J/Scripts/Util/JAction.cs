using UnityEngine;

namespace J
{

	[AddComponentMenu("J/Util/JAction")]
	public class JAction : MonoBehaviour {

		[Range(0.02f, 600f)]
		[SerializeField]	float delay = 0.2f;
		[SerializeField]	bool doOnStart = false;
		[SerializeField]	UnityEngine.Events.UnityEvent normalAction;
		[SerializeField]	UnityEngine.Events.UnityEvent delayedAction;


		void Start () {
			if (doOnStart) {
				CallNormalAction ();
				CallDelayedAction ();
			}
		}

		public void CallNormalAction () {
			normalAction.Invoke ();

		}
		public void CallDelayedAction () {
			Invoke ("CallDelayedActionPrivate", delay);
		}
		private void CallDelayedActionPrivate() {
			delayedAction.Invoke ();
		}
	}

}