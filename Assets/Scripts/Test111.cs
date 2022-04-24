using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Test111 : MonoBehaviour
{
    public Entity entity;
    public bool isHitRecover;
    public bool airborne;
    public bool fallDown;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (entity != null)
        {
            isHitRecover = entity.isHitRecover;
            airborne = entity.airborne;
            fallDown = entity.fallDown;
        }
    }

}
