using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : UserInput
{
    [SerializeField, Space]
    private KeyCode fire1;
    [SerializeField, Space]
    private KeyCode fire2;
    [SerializeField, Space]
    private KeyCode jump;

    protected override void ButtonUpdate()
    {
        MoveAxisX.Tick(Input.GetAxisRaw("Horizontal"));
        MoveAxisY.Tick(Input.GetAxisRaw("Vertical"));
        MoveLeft.Tick(Input.GetKey(KeyCode.LeftArrow));
        MoveRight.Tick(Input.GetKey(KeyCode.RightArrow));
        //CameraAxisX.Tick(Input.GetAxis("Mouse X"));
        //CameraAxisY.Tick(Input.GetAxis("Mouse Y"));
        ButtonFire1.Tick(Input.GetKey(fire1));
        ButtonFire2.Tick(Input.GetKey(fire2));
        ButtonJump.Tick(Input.GetKey(jump));
    }
}

