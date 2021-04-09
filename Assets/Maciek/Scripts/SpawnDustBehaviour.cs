using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDustBehaviour : MonoBehaviour
{
    public void AnimationEnd() {
        Destroy(gameObject);
    }
}
