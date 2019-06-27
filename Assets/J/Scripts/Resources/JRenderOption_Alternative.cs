using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class JRenderOption_AlternativeFactory : IRenderOptionFactory
    {
        private GameObject Blueprint;

        public JRenderOption_AlternativeFactory(GameObject InBlueprint)
        {
            if (!InBlueprint.GetComponent<JRenderOption_Alternative>())
            {
                Debug.LogError("Expected URenderOption_Alternative component on Blueprint GameObject, not found");
            }
            else
            {
                Blueprint = InBlueprint;
            }
        }

        public JRenderOption[] BuildRenderOptions(JResource.ContentOption[] Options)
        {
            List<JRenderOption> RenderOptions = new List<JRenderOption>();

            for (int i = 0; i < Options.Length; i++)
            {
                JResource.ContentOption Opt = Options[i];
                GameObject instantiated = GameObject.Instantiate(Blueprint);
                JRenderOption_Alternative alternative = instantiated.GetComponent<JRenderOption_Alternative>();

                alternative.Assign(Opt);
                RenderOptions.Add(alternative);
            }

            return RenderOptions.ToArray();
        }
    }

    [AddComponentMenu("J/Resources/RenderOptions/RenderOption_Alternative")]
    public class JRenderOption_Alternative : JRenderOption
    {

        public UnityEngine.UI.Toggle Toggle;
        public UnityEngine.UI.Text Label;
        public UnityEngine.UI.Text IndexLabel;

        private void Awake()
        {
            //Need to have bindings onto the Toggle
            Toggle.onValueChanged.AddListener(OnToggleValueChange);
        }

        private void OnToggleValueChange(bool Active)
        {
            AnswerValueChange(Active.ToString());
        }

        protected override void OnOwningOptionChanged(JResource.ContentOption Option)
        {
            //We should be assigned to a toggle, so we can search it and init the values
            Label.text = Option.GetValueAsString("texto");
            int asciiValue = (int)'A' + Option.Index;
            IndexLabel.text = ((char)asciiValue).ToString();

            //Check for answer
            bool bIsOn;
            bool bParseSuccess = bool.TryParse(Option.AnswerData,out bIsOn);
            Toggle.isOn = bParseSuccess ? bIsOn : false;
        }

        public override IRenderOptionFactory GetFactory()
        {
            return new JRenderOption_AlternativeFactory(this.gameObject);
        }

    }

}