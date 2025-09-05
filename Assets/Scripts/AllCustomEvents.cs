using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllCustomEvents : MonoBehaviour
{
}

[System.Serializable] public class OnFloatChange : UnityEvent<float> { }    
[System.Serializable] public class OnStringChange : UnityEvent<string> { }    
[System.Serializable] public class OnIntChange : UnityEvent<int> { }    
[System.Serializable] public class OnBoolChange : UnityEvent<bool> { }  
[System.Serializable] public class OnChange : UnityEvent { }    