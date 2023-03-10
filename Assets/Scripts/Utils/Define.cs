using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum MainMenuTab
    {
        Battle,
        Shop,
        Equipment,
        Trials,
        Evolve,
    }
    
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Preseed,
        PointerDown,
        PointerUp,
        Drag,
    }

    public enum MoveDir
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }

    public enum CreatureState
    {
        Idle,
        Moving,
        Skill,
        Dead,
    }

    public enum SkillType
    {
        Commendation,
        Spinner,
        Ball,
        Drone,
        Firebomb,
        Tree,
        Count
    }

    public enum EventTypeInt
    {
        Count
    }
    public enum EventTypeObj
    {
        DeadMonster,
        Count
    }
}
