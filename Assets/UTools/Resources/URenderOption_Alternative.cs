using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URenderOption_AlternativeFactory : IRenderOptionFactory
{
    private GameObject Blueprint;

    public URenderOption_AlternativeFactory(GameObject InBlueprint)
    {
        if(!InBlueprint.GetComponent<URenderOption_Alternative>())
        {
            Debug.LogError("Expected URenderOption_Alternative component on Blueprint GameObject, not found");
        }
        else
        {
            Blueprint = InBlueprint;
        }
    }

    public URenderOption[] BuildRenderOptions(UResource.ContentOption[] Options)
    {
        List<URenderOption> RenderOptions = new List<URenderOption>();

        for(int i = 0; i < Options.Length; i++)
        {
            UResource.ContentOption Opt = Options[i];
            GameObject instantiated = GameObject.Instantiate(Blueprint);
            URenderOption_Alternative alternative = instantiated.GetComponent<URenderOption_Alternative>();

            alternative.Assign(Opt, i);
            RenderOptions.Add(alternative);
        }

        return RenderOptions.ToArray();
    }
}

public class URenderOption_Alternative : URenderOption {

    public UnityEngine.UI.Toggle Toggle;
    public UnityEngine.UI.Text Label;
    public UnityEngine.UI.Text IndexLabel;

    public void Assign(UResource.ContentOption Option, int index)
    {
        //We should be assigned to a toggle, so we can search it and init the values
        Toggle.isOn = false;
        Label.text = Option.Data;
        int asciiValue = (int)'A' + index;
        IndexLabel.text = ((char)asciiValue).ToString();
    }

    public override IRenderOptionFactory GetFactory()
    {
        return new URenderOption_AlternativeFactory(this.gameObject);
    }

}
