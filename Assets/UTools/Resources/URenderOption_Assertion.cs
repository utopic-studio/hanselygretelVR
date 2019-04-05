using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URenderOption_AssertionFactory : IRenderOptionFactory
{
    private GameObject Blueprint;

    public URenderOption_AssertionFactory(GameObject InBlueprint)
    {
        if (!InBlueprint.GetComponent<URenderOption_Assertion>())
        {
            Debug.LogError("Expected URenderOption_Assertion component on Blueprint GameObject, not found");
        }
        else
        {
            Blueprint = InBlueprint;
        }
    }

    public URenderOption[] BuildRenderOptions(UResource.ContentOption[] Options)
    {
        List<URenderOption> RenderOptions = new List<URenderOption>();

        foreach (UResource.ContentOption Opt in Options)
        {
            GameObject instantiated = GameObject.Instantiate(Blueprint);
            URenderOption_Assertion alternative = instantiated.GetComponent<URenderOption_Assertion>();

            alternative.Assign(Opt);
            RenderOptions.Add(alternative);
        }

        return RenderOptions.ToArray();
    }
}

public class URenderOption_Assertion : URenderOption {

    public UnityEngine.UI.Toggle Toggle_True;
    public  UnityEngine.UI.Toggle Toggle_False;
    public UnityEngine.UI.Text Label;

    public override IRenderOptionFactory GetFactory()
    {
        return new URenderOption_AssertionFactory(this.gameObject);
    }

    public void Assign(UResource.ContentOption Option)
    {
        //We should be assigned to a toggle, so we can search it and init the values
        Toggle_True.isOn = false;
        Toggle_False.isOn = false;
        Label.text = Option.Data;
    }
}
