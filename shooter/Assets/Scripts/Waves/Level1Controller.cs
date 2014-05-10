﻿using UnityEngine;

using System;
using System.Collections;

public class Level1Controller : MonoBehaviour 
{
    public GameObject wave1EnemyA, wave1EnemyB, wave1EnemyC, wave1EnemyD;
    public GameObject singlePathEnemy1, singlePathEnemy2;
    public GameObject wave4EnemyA, wave4EnemyB, wave4EnemyC;
    public GameObject boss;

    private int currentEnemyCount;
    private int currentWave;

    private const int WAVE1_COUNT = 4;
    private const int SINGLE_PATH_WAVE_COUNT = 15;
    private const int WAVE4_PATH_COUNT = 10;
    private const float TIME_BETWEEN_WAVES = 1f;

    private bool isRunning;


    void Start()
    {
        currentEnemyCount = 4;
        currentWave = 1;
        isRunning = false;

        StartCoroutine(StartWave1());
    }

    void Update()
    {
        if (!isRunning && currentEnemyCount == 0) // Switch to next wave
        {
            currentWave++;
            switch (currentWave)
            {
                case 2:
                    currentEnemyCount = SINGLE_PATH_WAVE_COUNT;
                    StartSinglePathWave(singlePathEnemy1, SINGLE_PATH_WAVE_COUNT);
                    break;
                case 3:
                    currentEnemyCount = SINGLE_PATH_WAVE_COUNT;
                    StartSinglePathWave(singlePathEnemy2, SINGLE_PATH_WAVE_COUNT);
                    break;
                case 4:
                    currentEnemyCount = WAVE4_PATH_COUNT * 3;
                    StartCoroutine(StartWave4());
                    break;
                case 5:
                    currentEnemyCount = 1;
                    StartCoroutine(StartBoss());
                    break;
            }
        }
    }

    public void DecrementEnemyCount()
    {
        currentEnemyCount--;
    }

    private IEnumerator StartWave1()
    {
        isRunning = true;
        yield return new WaitForSeconds(1f);

        string pathName1 = wave1EnemyA.GetComponent<EnemyMovement>().pathName;
        string pathName2 = wave1EnemyB.GetComponent<EnemyMovement>().pathName;
        string pathName3 = wave1EnemyC.GetComponent<EnemyMovement>().pathName;
        string pathName4 = wave1EnemyD.GetComponent<EnemyMovement>().pathName;

        Instantiate(wave1EnemyA, iTweenPath.GetPath(pathName1)[0], wave1EnemyA.transform.rotation);
        Instantiate(wave1EnemyB, iTweenPath.GetPath(pathName2)[0], wave1EnemyB.transform.rotation);
        Instantiate(wave1EnemyC, iTweenPath.GetPath(pathName3)[0], wave1EnemyC.transform.rotation);
        Instantiate(wave1EnemyD, iTweenPath.GetPath(pathName4)[0], wave1EnemyD.transform.rotation);

        isRunning = false;
    }

    /// <summary>
    /// Start a wave that has multiple enemies on a single path.
    /// </summary>
    /// <param name="enemy">The enemy to put on the path</param>
    private void StartSinglePathWave(GameObject enemy, int count)
    {
        string pathName = enemy.GetComponent<EnemyMovement>().pathName;
        StartCoroutine(SpawnSinglePathEnemies(enemy, pathName, count));
    }

    private IEnumerator SpawnSinglePathEnemies(GameObject enemy, string pathName, int count)
    {
        isRunning = true;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(TIME_BETWEEN_WAVES);
            Instantiate(enemy, iTweenPath.GetPath(pathName)[0], enemy.transform.rotation);
        }

        isRunning = false;
    }

    private IEnumerator StartWave4()
    {
        isRunning = true;
        yield return new WaitForSeconds(TIME_BETWEEN_WAVES);

        for (int i = 0; i < WAVE4_PATH_COUNT; i++)
        {
            SpawnEnemyWithWait(wave4EnemyA);
            yield return new WaitForSeconds(TIME_BETWEEN_WAVES / 2);

            SpawnEnemyWithWait(wave4EnemyB);
            yield return new WaitForSeconds(TIME_BETWEEN_WAVES / 2);

            SpawnEnemyWithWait(wave4EnemyC);
            yield return new WaitForSeconds(TIME_BETWEEN_WAVES / 2);
        }

        isRunning = false;
    }

    private IEnumerator StartBoss()
    {
        yield return new WaitForSeconds(TIME_BETWEEN_WAVES);

        string pathName = boss.GetComponent<EnemyMovement>().pathName;
        Instantiate(boss, iTweenPath.GetPath(pathName)[0], boss.transform.rotation);
    }

    /// <summary>
    /// Instantiate an enemy then wait.
    /// </summary>
    /// <param name="enemy">The enemy to spawn</param>
    /// <returns>The IEnumerator for StartCoroutine</returns>
    private void SpawnEnemyWithWait(GameObject enemy)
    {
        string pathName = enemy.GetComponent<EnemyMovement>().pathName;
        Instantiate(enemy, iTweenPath.GetPath(pathName)[0], enemy.transform.rotation);
    }
}