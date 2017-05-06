using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public bool scrolling, parallax;

    public float size;//Background size
    public float parallaxSpd;

    private Transform cameraTransform;
    private Transform[] layers;
    private float view = 10;
    private int left;
    private int right;
    private float lastCameraX;

    public void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        left = 0;
        right = layers.Length - 1;
    }

    public void Update()
    {
        if (parallax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * parallaxSpd);
        }

        lastCameraX = cameraTransform.position.x;

        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[left].transform.position.x + view))
            {
                scrollLeft();
            }
            if (cameraTransform.position.x > (layers[right].transform.position.x - view))
            {
                scrollRight();
            }
        }
    }

    private void scrollLeft()
    {
        layers[right].position = Vector3.right * (layers[left].position.x - size);
        left = right;
        right--;
        if (right < 0)
        {
            right = layers.Length - 1;
        }
    }

    private void scrollRight()
    {
        layers[left].position = Vector3.right * (layers[right].position.x + size);
        right = left;
        left++;
        if (left == layers.Length)
        {
            left = 0;
        }
    }


}
