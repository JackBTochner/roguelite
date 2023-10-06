using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Interactor : MonoBehaviour
{
    public HubNPC currentTarget;
    public InputReader inputReader;

    private InteractCanvasManager _interactCanvasManager;

    [SerializeField]
    private TransformAnchor _interactCanvasTransformAnchor;

    private void OnEnable()
    { 
        inputReader.OnInteractPerformed += AttemptInteract;
    }
    private void OnDisable()
    { 
        inputReader.OnInteractPerformed -= AttemptInteract;
        currentTarget = null;
        _interactCanvasManager = null;
    }

    void Update()
    {
        if (_interactCanvasManager == null && _interactCanvasTransformAnchor.isSet)
        {
            _interactCanvasManager = _interactCanvasTransformAnchor.Value.GetComponent<InteractCanvasManager>();
        }
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        { 
            if (hit.transform.CompareTag("Interactable"))
            {
                currentTarget = hit.transform.GetComponent<HubNPC>();
                if (!currentTarget.isInteracting && currentTarget.canInteract) { 
                    if (_interactCanvasTransformAnchor.isSet)
                        {
                            _interactCanvasManager.EnableInteractPrompt(currentTarget.interactPrompt);
                        }
                }
            }
        }
        else
        {
            if(currentTarget)
                ClearCurrentTarget();
            if (_interactCanvasTransformAnchor.isSet)
                _interactCanvasManager.DisableInteractPrompt();
        }
    }

    public void AttemptInteract()
    {
        if (currentTarget)
        {
            if (currentTarget.canInteract)
            {
                currentTarget.StartConversation(this);
                if(_interactCanvasTransformAnchor.isSet)
                    _interactCanvasManager.DisableInteractPrompt();
            }
        }
    }

    public void ClearCurrentTarget()
    { 
        currentTarget = null;
    }
}
