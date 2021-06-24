using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class InteractFunc : MonoBehaviour
{
    
    public GameObject prefab;
    private GameObject clone;
    public bool dialogue = false;
    public string[] text;
    public bool playSecondPrefab = false;
    public GameObject prefab2;
    public virtual void GetText(string tekst) {
        TextMeshProUGUI text = gameObject.AddComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        text.text = tekst;
    }
    public virtual void MainFunction() {
        if (clone)
        {
            clone.SetActive(true);
            if (dialogue) {
                clone.transform.SetParent(transform);
                clone.GetComponent<Window_Dialogue>().SetText(text);
            }
            Debug.Log("Halo jestem");
        }
        else {
            clone = Instantiate(prefab, new Vector3(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            clone.SetActive(true);

            if (dialogue) {
                clone.transform.SetParent(transform);
                clone.GetComponent<Window_Dialogue>().SetText(text);
            }
            Debug.Log("nie ma mnie");
        }
        if (playSecondPrefab) {
            StartCoroutine(SpawnSecondPrefab(.2f));
        }
    }
    private IEnumerator SpawnSecondPrefab(float time) {
        yield return new WaitForSeconds(time);
        clone = Instantiate(prefab2, new Vector3(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        clone.SetActive(true);
    }
    public virtual void DestroyInteract() {
        Object.Destroy(clone);
        Object.Destroy(this.GetComponent<TextMeshProUGUI>());
    }
}
