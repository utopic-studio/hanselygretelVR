using UnityEngine;

namespace J
{

	[AddComponentMenu("J/UI/JFadeUI")]
	[RequireComponent(typeof(UnityEngine.CanvasGroup))]
	public class JFadeUI : MonoBehaviour {

		enum UIAlphaStartMode {invisible, visible, visibleFadeIn, invisibleFadeOut}

		[SerializeField]	UIAlphaStartMode startVisibility = UIAlphaStartMode.visible;
		[SerializeField]	float fadeInTime = 1f;
		[SerializeField]	float fadeOutTime = 1f;

		private UnityEngine.CanvasGroup canvasGroup;

		void Start () {
			canvasGroup = GetComponent<UnityEngine.CanvasGroup> ();

			switch (startVisibility) {
			case UIAlphaStartMode.invisible:
				HideInstantly ();
				break;
			case UIAlphaStartMode.visible:
				ShowInstantly ();
				break;
			case UIAlphaStartMode.visibleFadeIn:
				Show ();
				break;
			case UIAlphaStartMode.invisibleFadeOut:
				Hide ();
				break;
			default:
				break;
			}
		}

		public void Show() {
			J.instance.followCurve (x => canvasGroup.alpha = x, duration: fadeInTime, repeat: 1, type: CurveType.Linear);
		}
		public void Hide() {
			J.instance.followCurve (x => canvasGroup.alpha = x, duration: fadeOutTime, repeat: 1, type: CurveType.Linear, reverse: true);
		}
		public void ShowInstantly() {
			canvasGroup.alpha = 1f;
		}
		public void HideInstantly() {
			canvasGroup.alpha = 0f;
		}

		public void SetFadeInTime(float f) {
			this.fadeInTime = f;
		}
		public void SetFadeOutTime(float f) {
			this.fadeOutTime = f;
		}
	}

}