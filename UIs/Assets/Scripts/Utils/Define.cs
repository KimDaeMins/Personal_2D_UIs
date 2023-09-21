using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        Drag,
    }

    public enum Scene
    {
        Unknown,
        SampleScene,
        MainScene,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Speech,
        Max,
    }
}