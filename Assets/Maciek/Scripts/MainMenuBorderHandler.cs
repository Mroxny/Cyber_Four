using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBorderHandler : MonoBehaviour
{
    void Update()
    {
        float x = Screen.width / 90f;
        float y = Screen.height / 110f;
        gameObject.transform.Find("Border_1").position = new Vector2(-x, y);
        gameObject.transform.Find("Border_2").position = new Vector2(x, y);
        gameObject.transform.Find("Border_3").position = new Vector2(-x, -y);
        gameObject.transform.Find("Border_4").position = new Vector2(x, -y);
    }
}
