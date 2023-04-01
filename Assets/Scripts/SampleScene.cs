using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ST;

public class SampleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindObjectOfType<SRTDisplayer>().Begin());
    }
}
