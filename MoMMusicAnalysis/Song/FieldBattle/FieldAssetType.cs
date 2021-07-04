namespace MoMMusicAnalysis
{
    public enum FieldAssetType
    {
        None = 0,
        AerialCommonArrow = 2,
        AerialUncommonArrow = 4,
        MultiHitAerialArrow = 6,
        ShooterProjectileArrow = 7,
        AerialShooterProjectileArrow = 8,
        AerialShooterArrow = 8,
        JumpingAerialArrow = 10,
        CrystalRightLeft = 13, // Jump Flag == 0, Unk1 always equals 1 with these?
        CrystalCenter = 14, // Jump Flag == 0
        GlideArrow = 15,
    }
}