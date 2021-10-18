using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Character/CharacterAttribute")]
public class CharacterAttribute : ScriptableObject
{
    public float HP;

    public float MP;

    public float Aggressivity;

    public float Defensive;

    public float MoveSpeed;

    public float JumpPower;
}
