using System;
using UnityEngine;

[Serializable]
public class Gun
{
    [HideInInspector] public string Name = "Gun";
    public GunType gunType;
    public Animator gunAnimator;
}
