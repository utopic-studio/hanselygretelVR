using UnityEngine;

namespace J
{

    public class JTimeScale : MonoBehaviour
    {
        [Range(0f,10f)]
        [SerializeField] float timeScale = 1f;
        
        private void OnValidate()
        {
            Time.timeScale = timeScale;
        }
        
    }

}