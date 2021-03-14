using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class InteractFunc : MonoBehaviour
{
    public virtual void GetText(string tekst) {
        TextMeshProUGUI text = gameObject.AddComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        text.text = tekst;
    }
    public abstract void MainFunction();

    public virtual void DestroyInteract() {
        Object.Destroy(this.GetComponent<TextMeshProUGUI>());
    }
}
