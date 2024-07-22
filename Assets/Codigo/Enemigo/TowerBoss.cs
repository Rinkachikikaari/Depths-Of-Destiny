using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBoss : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float detectionRange = 10f;
    public int bulletPattern = 0; // 0: Circle, 1: Cross, 2: Spiral
    private bool crossPattern = true; // Variable para alternar entre cruz y X

    private Transform player;
    private float nextFireTime = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ChangePattern());
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            if (Time.time >= nextFireTime)
            {
                ShootPattern();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    private void ShootPattern()
    {
        switch (bulletPattern)
        {
            case 0:
                ShootCirclePattern();
                break;
            case 1:
                ShootCrossPattern();
                break;
            case 2:
                StartCoroutine(ShootSpiralPattern());
                break;
        }
    }

    private void ShootCirclePattern()
    {
        int bullets = 8;
        float angleStep = 360f / bullets;
        float angle = 0f;

        for (int i = 0; i < bullets; i++)
        {
            float projectileDirX = Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirY = Mathf.Cos((angle * Mathf.PI) / 180);

            Vector2 projectileMoveDirection = new Vector2(projectileDirX, projectileDirY).normalized;

            var proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            ProjectileBoss projectileScript = proj.GetComponent<ProjectileBoss>();
            if (projectileScript != null)
            {
                projectileScript.SetVelocity(projectileMoveDirection);
            }
            else
            {
                Debug.LogError("Projectile script is missing from the projectile prefab!");
            }

            angle += angleStep;
        }
    }

    private void ShootCrossPattern()
    {
        if (crossPattern)
        {
            Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            foreach (Vector2 direction in directions)
            {
                var proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                ProjectileBoss projectileScript = proj.GetComponent<ProjectileBoss>();
                if (projectileScript != null)
                {
                    projectileScript.SetVelocity(direction);
                }
                else
                {
                    Debug.LogError("Projectile script is missing from the projectile prefab!");
                }
            }
        }
        else
        {
            Vector2[] directions = { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
            foreach (Vector2 direction in directions)
            {
                var proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                ProjectileBoss projectileScript = proj.GetComponent<ProjectileBoss>();
                if (projectileScript != null)
                {
                    projectileScript.SetVelocity(direction);
                }
                else
                {
                    Debug.LogError("Projectile script is missing from the projectile prefab!");
                }
            }
        }
        crossPattern = !crossPattern; // Alternar el patrón
    }

    private IEnumerator ShootSpiralPattern()
    {
        int bullets = 12;
        float angleStep = 30f;
        float angle = 0f;

        for (int i = 0; i < bullets; i++)
        {
            float projectileDirX = Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirY = Mathf.Cos((angle * Mathf.PI) / 180);

            Vector2 projectileMoveDirection = new Vector2(projectileDirX, projectileDirY).normalized;

            var proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            ProjectileBoss projectileScript = proj.GetComponent<ProjectileBoss>();
            if (projectileScript != null)
            {
                projectileScript.SetVelocity(projectileMoveDirection);
            }
            else
            {
                Debug.LogError("Projectile script is missing from the projectile prefab!");
            }

            angle += angleStep;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ChangePattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            bulletPattern = (bulletPattern + 1) % 3;
        }
    }
}