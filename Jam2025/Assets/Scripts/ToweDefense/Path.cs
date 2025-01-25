using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();

    public List<Transform> Waypoints => _waypoints;

}
