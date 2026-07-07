public enum ThreatGroup { CosmicHazard, Pirates, Imperium, Xenolife }

public class EnemyData
{
    public ThreatGroup ThreatGroup { get; set; }
    public string PrefabPath { get; set; }
    public float Speed { get; set; } = 1f;
    public int Damage { get; set; } = 20;
}

public class Asteroid : EnemyData
{
    public Asteroid()
    {
        ThreatGroup = ThreatGroup.CosmicHazard;
        PrefabPath = "Prefabs/Asteroids/asteroid_1";
        Speed = 50f;
        Damage = 20;
    }
}