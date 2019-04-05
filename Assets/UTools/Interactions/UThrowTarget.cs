using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnThrowableReachedTargetSignature : UnityEvent<UThrowable>
{
}

/// <summary>
/// Manages a throw area that reacts to specific UThrowable objects being thrown to it.
/// </summary>
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
[AddComponentMenu("UTools/Interactions/UThrowTarget")]
public class UThrowTarget : MonoBehaviour
{
    /// <summary>
    /// Called when a throwable reaches this target.
    /// </summary>
    public OnThrowableReachedTargetSignature OnThrowableReachedTarget;

    //Cached components
    private Renderer _renderer;
    private Collider _collider;

    /// <summary>
    /// Marks if the throw target has received a throwable object
    /// </summary>
    private bool _success = false;
    

    // Start is called before the first frame update
    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        UThrowable _throwable = other.GetComponent<UThrowable>();

        if (_throwable)
        {
            _success = true;
            
            if(OnThrowableReachedTarget != null)
            {
                OnThrowableReachedTarget.Invoke(_throwable);
            }
        }
    }

    /// <summary>
    /// Used by the throwable to validate the throw onto this target or rollback if it didn't reach
    /// </summary>
    /// <param name="InThrowable"></param>
    public void ValidateThrow(UThrowable InThrowable)
    {
        if (!_success)
        {
            InThrowable.RollbackThrow();
        }
    }
}
