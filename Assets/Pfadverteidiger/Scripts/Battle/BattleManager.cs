using System.Collections.Generic;
using System;

public static class BattleManager
{
    private static List<EnemyData> _enemiesToSpawn = new List<EnemyData>();

    public static void PrepareBattle()
    {
        for (int i = 0; i < 10; i++)
        {
            _enemiesToSpawn.Add(new Asteroid());
        }
    }

    public static EnemyData GetNextEnemy()
    {
        if (_enemiesToSpawn.Count == 0)
        {
            return null;
        }

        EnemyData nextEnemy = _enemiesToSpawn[0];
        _enemiesToSpawn.RemoveAt(0);
        return nextEnemy;
    }
}