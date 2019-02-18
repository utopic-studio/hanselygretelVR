using UnityEngine;
using System.Collections;

namespace J
{

    [AddComponentMenu("J/Util/JAction")]
    public class JAction : MonoBehaviour
    {

        [Range(0.02f, 600f)]
       
        [SerializeField] bool doOnStart = false;
        [SerializeField] UnityEngine.Events.UnityEvent normalAction;
        [SerializeField] UnityEngine.Events.UnityEvent[] delayedAction;
        [SerializeField] float[] delay;

        void Start()
        {
            if (doOnStart)
            {
                CallNormalAction();
                CallDelayedAction();
            }
        }
        public void CallBothActions()
        {
            CallNormalAction();
            CallDelayedAction();
        }
        public void CallNormalAction()
        {
            normalAction.Invoke();

        }
        public void CallDelayedAction()
        {
            for (int i = 0; i < delay.Length; i++)
            {
                //Invoke("CallDelayedActionPrivate", delay[i]);
                StartCoroutine("CallDelayedActionPrivate",i);
                
                
            }
            
        }
        IEnumerator CallDelayedActionPrivate(int ActionNumber)
        {
            yield return new WaitForSeconds(delay[ActionNumber]);
            delayedAction[ActionNumber].Invoke();
  
        }
    }

}