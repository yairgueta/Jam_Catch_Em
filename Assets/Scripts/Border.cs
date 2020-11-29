using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public float size = 500;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var position = transform.position;
        var c = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        var s = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        Vector3 v = new Vector3(position.x - size*c, position.y, position.z-size*s);
        Gizmos.DrawLine(v, v + new Vector3(c, 0, s) * 2 * size);
    }
}
