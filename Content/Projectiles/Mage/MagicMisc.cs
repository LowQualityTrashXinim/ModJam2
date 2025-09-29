using Terraria;
using Terraria.ID;
using ModJam2.Common.Utils;

namespace ModJam2.Content.Projectiles.Mage;
public class Held_DemonScythe : HeldProjectile
{
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.DemonScythe);
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override void SetHostileDefaults()
    {
        Projectile.width = Projectile.height = ModUtils.Get_ItemTextureSize(ItemID.DemonScythe).X;
        ShootProjectile = ProjectileID.DemonScythe;
        shootVel = 1f;
        Projectile.damage = 60;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}
public class Held_WaterBolt : HeldProjectile
{
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.WaterBolt);
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override void SetHostileDefaults()
    {
        Projectile.width = Projectile.height = ModUtils.Get_ItemTextureSize(ItemID.WaterBolt).X;
        ShootProjectile = ProjectileID.WaterBolt;
        shootVel = 3f;
        Projectile.damage = 40;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}
public class Held_CursedBook : HeldProjectile
{
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.BookofSkulls);
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override void SetHostileDefaults()
    {
        Projectile.width = Projectile.height = ModUtils.Get_ItemTextureSize(ItemID.BookofSkulls).X;
        ShootProjectile = ProjectileID.Skull;
        shootVel = 1f;
        Projectile.damage = 40;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}