using JetBrains.Annotations;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform ShotPoint;
    public int damage;
    public LineRenderer lineRenderer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine (Shoot());
        }
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(ShotPoint.position, ShotPoint.right);
        if (hit)
        {
            Enemy Enemy = hit.transform.GetComponent<Enemy>();
            if (Enemy != null) 
            {
                Enemy.TakeDamage(damage);
            }
            lineRenderer.SetPosition(0, ShotPoint.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, ShotPoint.position);
            lineRenderer.SetPosition(1, ShotPoint.position + ShotPoint.right * 100);
        }

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
    }
}
