using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    public float radius = 5;
    private void Start()
    {
        Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y), radius, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Interactable>()) DoInteract(collision.collider.GetComponent<Interactable>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>()) DoInteract(collision.GetComponent<Interactable>());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Interactable>()) collision.collider.GetComponent<Interactable>().DestroyInteract();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>()) collision.GetComponent<Interactable>().DestroyInteract();
    }

    private void DoInteract(Interactable interakcja) {
        Debug.Log("AAAAA");
        interakcja.DoFunc();
    }
}
