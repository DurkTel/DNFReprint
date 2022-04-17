using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamage
{
    float hurt { get; }
    GameObject hurtObj { get; set; }
    Transform hurtTransform { get; }
}
