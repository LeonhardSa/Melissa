using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSpinner : MonoBehaviour
{
    public float speed = 20f;
    Transform from;
    Transform to;

    //public Color RingColor;
    //Renderer renderer;
    //Material material;

    //void Start()
    //{
    //    renderer = GetComponent<Renderer>();
    //    material = renderer.material;
    //}

    void Update()
    {
        gameObject.transform.Rotate(0, 0, 1 *100* speed * Time.deltaTime);
        from = gameObject.transform;
    }
}
