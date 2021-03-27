using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class InteractFunc : MonoBehaviour
{
    public GameObject prefab;
    private GameObject clone;
    public virtual void GetText(string tekst) {
        TextMeshProUGUI text = gameObject.AddComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        text.text = tekst;
    }
    public virtual void MainFunction() {
        if (clone)
        {
            clone.SetActive(true);
            Debug.Log("Halo jestem");
        }
        else {
            clone = Instantiate(prefab, new Vector3(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            clone.SetActive(true);
            Debug.Log("nie ma mnie");
        }
        
    }

    public virtual void DestroyInteract() {
        Object.Destroy(clone);
        Object.Destroy(this.GetComponent<TextMeshProUGUI>());
    }
}
