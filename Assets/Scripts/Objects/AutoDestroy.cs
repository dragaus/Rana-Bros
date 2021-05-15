using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AutoDestroy : MonoBehaviour
{
    public void AutoDestruction() 
    {
        Destroy(this.gameObject);
    }
}
