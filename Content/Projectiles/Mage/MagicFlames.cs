using Microsoft.Xna.Framework;
using ModJam2.Common.Utils;
using Terraria;
using Terraria.ID;

namespace ModJam2.Content.Projectiles.Mage;

public class Held_FlowerOfFire : HeldProjectile
{
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.FlowerofFire);
    public override void SetHostileDefaults()
    {
        Point size = ModUtils.Get_ItemTextureSize(ItemID.FlowerofFire);
        Projectile.width = size.X;
        Projectile.height = size.Y;
        ExtraRotationValue = MathHelper.PiOver4;
        ShootProjectile = ProjectileID.BallofFire;
        shootVel = 7f + Main.rand.NextFloat(-2,2); Projectile.damage = 43;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}
public class Held_FlowerOfFrost : HeldProjectile
{
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.FlowerofFrost);
    public override void SetHostileDefaults()
    {
        Point size = ModUtils.Get_ItemTextureSize(ItemID.FlowerofFrost);
        Projectile.width = size.X;
        Projectile.height = size.Y;
        ExtraRotationValue = MathHelper.PiOver4;
        ShootProjectile = ProjectileID.BallofFrost;
        shootVel = 7f + Main.rand.NextFloat(-2, 2); Projectile.damage = 43;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}

public class Held_CursedFlames : HeldProjectile
{
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.CursedFlames);
    public override void SetHostileDefaults()
    {
        Point size = ModUtils.Get_ItemTextureSize(ItemID.CursedFlames);
        Projectile.width = size.X;
        Projectile.height = size.Y;
        ShootProjectile = ProjectileID.CursedFlameHostile;
        shootVel = 7f + Main.rand.NextFloat(-2, 2); Projectile.damage = 43;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}