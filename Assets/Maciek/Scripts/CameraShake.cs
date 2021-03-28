using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude, Transform pos) {
        //transform.position = pos;
        Vector2 orginalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration) {
            float x = transform.localPosition.x + (Random.Range(-1f, 1f) * magnitude);
            float y = transform.localPosition.y + (Random.Range(-1f, 1f) * magnitude);
            transform.localPosition = new Vector2(x, y);
            elapsed += Time.deltaTime;

            yield return null;

        }
        transform.localPosition = orginalPos;
    }
}
