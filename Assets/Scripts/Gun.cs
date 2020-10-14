using System;
using UnityEngine;

[Serializable]
public class Gun
{
    [HideInInspector] public string Name = "Gun";
    public GunType gunType;
    //public GunEffects gunEffects;
    public Animator gunAnimator;
}
