using UnityEngine;

namespace J
{

    [AddComponentMenu("J/3D/JMove")]
    public class JLookAt : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] bool x, y=true, z;

        private void OnValidate()
        {
            if (!target)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        void Update()
        {
            transform.LookAt(target);
            Vector3 rotation = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
            if (!x)
                rotation.x = 0f;
            if (!y)
                rotation.y = 0f;
            if (!z)
                rotation.z = 0f;

            transform.rotation = Quaternion.Euler(rotation);
        }
    }

}