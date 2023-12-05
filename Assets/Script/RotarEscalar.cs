using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarEscalar : MonoBehaviour
{
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 initialScale;
    private float rotationSpeed = 0.2f;
    private float scaleFactor = 0.001f;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Gestión de rotación por desplazamiento
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Rotate(Vector3.up * touchDeltaPosition.x * rotationSpeed);
        }

        // Gestión de escalado por pellizco (pinch)
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Vector3 newScale = transform.localScale + Vector3.one * deltaMagnitudeDiff * scaleFactor;

            // Limita la escala mínima y máxima si es necesario
            newScale.x = Mathf.Clamp(newScale.x, 0.1f, 2.0f);
            newScale.y = Mathf.Clamp(newScale.y, 0.1f, 2.0f);
            newScale.z = Mathf.Clamp(newScale.z, 0.1f, 2.0f);

            transform.localScale = newScale;
        }
    }
}

