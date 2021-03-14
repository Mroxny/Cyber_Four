using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class PlayerInteraction : MonoBehaviour
{

    public float InteractionDistance;

    public TMPro.TextMeshProUGUI interactionText;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        RaycastHit hit;
        bool isHit = false;

        if (Physics.Raycast(ray, out hit, InteractionDistance)) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null) {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                isHit = true;
            }
        }

        if (!isHit) interactionText.text = "";
    }

    void HandleInteraction(Interactable interactable) {

        KeyCode key = KeyCode.F;
        switch (interactable.interactionType) {
            case Interactable.InteractionType.click:
                if (Input.GetKeyDown(key)) {
                    interactable.interact();
                }
                break;
            case Interactable.InteractionType.hold:
                if (Input.GetKeyDown(key))
                {
                    interactable.interact();
                }
                break;
        }
    }
}
*/