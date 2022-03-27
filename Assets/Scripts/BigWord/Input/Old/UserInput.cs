using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserInput : MonoBehaviour
{
    public static UserInput Instance;

    public InputAxis MoveAxisX = new InputAxis();

    public InputAxis MoveAxisY = new InputAxis();

    public InputAxis CameraAxisX = new InputAxis();

    public InputAxis CameraAxisY = new InputAxis();

    public InputButton ButtonFire1 = new InputButton();

    public InputButton ButtonFire2 = new InputButton();

    public InputButton ButtonJump = new InputButton();

    public InputButton MoveLeft = new InputButton();

    public InputButton MoveRight = new InputButton();


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ButtonUpdate();
    }

    protected abstract void ButtonUpdate();

    public InputButton GetButtonFormKeyEnum(InputKeys inputKeys)
    {
        switch (inputKeys)
        {
            case InputKeys.MoveAxisX:
                return MoveAxisX;
            case InputKeys.MoveAxisY:
                return MoveAxisY;
            case InputKeys.CameraAxisX:
                return CameraAxisX;
            case InputKeys.CameraAxisY:
                return CameraAxisY;
            case InputKeys.ButtonFire1:
                return ButtonFire1;
            case InputKeys.ButtonFire2:
                return ButtonFire2;
            case InputKeys.ButtonJump:
                return ButtonJump;
            case InputKeys.MoveLeft:
                return MoveLeft;
            case InputKeys.MoveRight:
                return MoveRight;
        }

        return ButtonFire1;
    }
}

public enum InputKeys
{
    MoveAxisX,
    MoveAxisY,
    CameraAxisX,
    CameraAxisY,
    ButtonFire1,
    ButtonFire2,
    ButtonJump,
    MoveLeft,
    MoveRight
}
