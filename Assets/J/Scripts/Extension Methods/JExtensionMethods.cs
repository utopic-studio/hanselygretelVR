using UnityEngine;

namespace J
{
		
	public static class JExtensionMethods {



		// :: GAME OBJECT ::
		public static void JToggleActive(this GameObject go) {
			go.SetActive (!go.activeSelf);
		}

		public static GameObject JCreateEmptyGameObjectNoDuplicates(this GameObject go, string name) {
			GameObject newGameObject;
			newGameObject = GameObject.Find(name);
			if (!newGameObject) {
				newGameObject = new GameObject(name);
			}
			return newGameObject;
		}




		// :: TRANSFORM ::
		public static void JReset(this Transform t) {
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}
		public static void JSetX(this Transform t, float x) {
			t.position = new Vector3(x, t.position.y, t.position.z);
		}
		public static void JSetY(this Transform t, float y) {
			t.position = new Vector3(t.position.x, y, t.position.z);
		}
		public static void JSetZ(this Transform t, float z) {
			t.position = new Vector3(t.position.x, t.position.y, z);
		}
		public static Transform JGetParent(this Transform t) {
			if (!t.parent)
				Debug.LogWarning(string.Format("J - No parent found for object {0}", t.gameObject.name));
			return t.parent;
		}






		// :: COLLIDER ::
		public static void JToggleCollider(this Collider col) {
			col.enabled = !col.enabled;
		}
		public static void JToggleIsTrigger(this Collider col) {
			col.isTrigger = !col.isTrigger;
		}







		// :: RENDERER ::
		public static void JToggleRenderer(this Renderer rend) {
			rend.enabled = !rend.enabled;
		}








		// :: RIGIDBODY ::
		public static void JToggleUseGravity(this Rigidbody rb) {
			rb.useGravity = !rb.useGravity;
		}
		public static void JToggleIsKinematic(this Rigidbody rb) {
			rb.isKinematic = !rb.isKinematic;
		}
	}

}