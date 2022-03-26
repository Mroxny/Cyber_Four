using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Shortcut : MonoBehaviour
{
    public void StartHurting() {
        transform.GetComponentInParent<Boss2>().StartHurting();
    }

    public void StopHurting()
    {
        transform.GetComponentInParent<Boss2>().StopHurting();
    }
}
