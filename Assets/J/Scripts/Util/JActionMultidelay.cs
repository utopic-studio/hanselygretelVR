using UnityEngine;
using System.Collections;

namespace J
{

    [AddComponentMenu("J/Util/JActionMultidelay")]
    public class JActionMultidelay : MonoBehaviour
    {

       
        [SerializeField] bool doOnStart = false;
        [SerializeField] UnityEngine.Events.UnityEvent normalAction;
        [SerializeField] UnityEngine.Events.UnityEvent[] delayedAction;
        [SerializeField] float[] delay;

        private int index;

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
                //Coroutine c = StartCoroutine(CallDelayedActionPrivate(i));
                index = i;
                Invoke("CallDelayedActionPrivate", i);
            }
            
        }
        
        //IEnumerator CallDelayedActionPrivate(int ActionNumber)
        void CallDelayedActionPrivate()
        {
            delayedAction[index].Invoke();

            //yield return new WaitForSeconds(delay[ActionNumber]);
            //delayedAction[ActionNumber].Invoke();

        }
    }

}