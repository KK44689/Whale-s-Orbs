using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToScreen : MonoBehaviour
{
    private Camera mainCamera;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        ScaleToScreenSize();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ScaleToScreenSize()
    {
        float height = 2.0f * Mathf.Tan(0.5f * mainCamera.fieldOfView) * 10;

        float width = height * Screen.width / Screen.height;

        transform.localScale = new Vector3(width, 120, height);
    }
}
