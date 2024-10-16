using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JKFrame;
public class InputControler : SingletonMono<InputControler>
{//这是通过摇杆传递变量的
    //static InputControler instance;
    public Joystick joystick;
    public float horizontalDeah;
    public float VerticalDeah;
    public Vector2 direction;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        float hDirection = Mathf.Abs(joystick.Horizontal) < horizontalDeah ? 0 : joystick.Horizontal;
        float vDirection = Mathf.Abs(joystick.Vertical) < horizontalDeah ? 0 : joystick.Vertical;
        direction = new Vector2(hDirection, vDirection).normalized;
        //new Vector2(0, vDirection).normalized + new Vector2(hDirection, 0).normalized;
       // Debug.Log(direction);
    }
    public Vector2 getDirection()
    {
        return direction;
    }
}
