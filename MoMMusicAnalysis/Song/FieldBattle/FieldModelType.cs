namespace MoMMusicAnalysis
{
    public enum FieldModelType 
    {
        None = 0,
        CommonEnemy = 1,
        AerialCommonEnemy = 2,
        UncommonEnemy = 3,
        AerialUncommonEnemy = 4,
        MultiHitGroundEnemy = 5,
        MultiHitAerialEnemy = 6,
        EnemyShooterProjectile = 7, // Note Type 2
        EnemyShooter = 7, // Note Type 0
        AerialEnemyShooterProjectile = 8, // Note Type 2
        AerialEnemyShooter = 8, // Note Type 0
        JumpingGroundEnemy = 9,
        JumpingAerialEnemy = 10,
        HiddenEnemy = 11, // Shadow is in the ground, (Maybe moving away from the player?)
        HittableAerialUncommonEnemy = 12,
        CrystalEnemyLeftRight = 13, // Note Type 1, Lane FarCenterRight?
        CrystalEnemyCenter = 14, // Note Type 1, Lane Center?
        GlideNote = 15, // Note Type 3
        Barrel = 16, // Note Type 4
        Crate = 16, // Note Type 4, Bytes 9 - 12 = 01 00 00 00 
        CrystalTeamHeal = 18 // Note Type 1
    }
}