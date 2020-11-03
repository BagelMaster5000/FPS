using System;
using UnityEngine;

[Serializable]
public class Gun
{
    [HideInInspector] public string name = "Gun";
    public GunType gunType;
    public Animator gunAnimator;
}
