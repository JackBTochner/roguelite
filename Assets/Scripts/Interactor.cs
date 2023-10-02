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
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 1.5f, transform.forward, out hit, 1.5f))
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                currentTarget = hit.transform.GetComponent<HubNPC>();
                if (!currentTarget.isInteracting && currentTarget.canInteract) { 
                    if (_interactCanvasTransformAnchor.Value != null)
                        {
                            _interactCanvasManager.EnableInteractPrompt(currentTarget.interactPrompt);
                        }
                }
            }
        }
        else
        {
            ClearCurrentTarget();
            _interactCanvasManager.DisableInteractPrompt();
        }

        if (_interactCanvasManager == null && _interactCanvasTransformAnchor.Value != null)
        {
            _interactCanvasManager = _interactCanvasTransformAnchor.Value.GetComponent<InteractCanvasManager>();
        }
    }

    public void AttemptInteract()
    {
        if (currentTarget)
        {
            if (currentTarget.canInteract)
            {
                currentTarget.StartConversation(this);
                _interactCanvasManager.DisableInteractPrompt();
            }
        }
    }

    public void ClearCurrentTarget()
    { 
        currentTarget = null;
    }
}
