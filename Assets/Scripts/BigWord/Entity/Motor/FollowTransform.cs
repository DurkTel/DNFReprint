using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform tagter;

    void Update()
    {
        transform.position = tagter.position;
    }
}
