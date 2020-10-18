using System;
using UnityEngine.Events;

[Serializable] public class VoidEvent : UnityEvent { }
[Serializable] public class BoolEvent : UnityEvent<bool> { }
[Serializable] public class FloatEvent : UnityEvent<float> { }
[Serializable] public class IntEvent : UnityEvent<int> { }
[Serializable] public class StringEvent : UnityEvent<string> { }