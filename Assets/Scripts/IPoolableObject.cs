using UnityEngine;
using System.Collections;

public interface IPoolableObject 
{
    GameObject gameObject { get; }
    string name { get; }
}
