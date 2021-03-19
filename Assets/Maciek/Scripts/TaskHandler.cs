using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskHandler : MonoBehaviour
{
    public Animator animator;
    [SerializeField]
    public TextMeshProUGUI task;
    GameObject button;
    public void Notify(string text, float time) {
        task.text = text;
        button = GameObject.Find("TaskButton");
        button.GetComponent<Button>().interactable = false;
        button.GetComponent<CanvasGroup>().alpha = 0.25f;
        animator.SetTrigger("Notify");
        StartCoroutine(Count(time));
    }
    IEnumerator Count(float t) {
        if (t == 0) {
            yield return null;
        }
        else{
            yield return new WaitForSeconds(t);
            HideNote();
        }
    }
    public void HideNote() {
        animator.SetTrigger("Hide");
        StartCoroutine(TurnOff(1));
    }
    IEnumerator TurnOff(float t) {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
        button.GetComponent<Button>().interactable = true;
        button.GetComponent<CanvasGroup>().alpha = 1;
    }
}
