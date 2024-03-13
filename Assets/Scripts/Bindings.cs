// TODO

using System.Collections.Generic;
using UnityEngine;

public enum Controls
{
    // movement
    KeyMoveUp,
    KeyMoveDown,
    KeyMoveLeft,
    KeyMoveRight,
    KeySprint
}

public class Bindings
{
    private readonly Dictionary<Controls, KeyCode> _bindingTable = new Dictionary<Controls, KeyCode>
    {
        {Controls.KeyMoveUp, KeyCode.Z},
        {Controls.KeyMoveDown, KeyCode.S},
        {Controls.KeyMoveLeft, KeyCode.Q},
        {Controls.KeyMoveRight, KeyCode.D},
        {Controls.KeySprint, KeyCode.LeftShift}
    };

    public KeyCode GetKeyCode(Controls control)
        // Return the keycode associated with a control
    {
        return _bindingTable[control];
    }
}