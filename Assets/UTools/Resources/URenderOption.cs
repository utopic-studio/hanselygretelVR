using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRenderOptionFactory
{
    URenderOption[] BuildRenderOptions(UResource.ContentOption[] Options);
}

public abstract class URenderOption : MonoBehaviour {

    //public abstract void Assign(Recurso.OpcionContenido Option);
    public abstract IRenderOptionFactory GetFactory();

    //TODO: GetData deberia devolver un objeto tipo Respuesta
}
