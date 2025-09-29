using Microsoft.Xna.Framework;
using ModJam2.Common.Utils;
using Terraria;
using Terraria.ID;

namespace ModJam2.Content.Projectiles.Mage;

public class Held_AmethystStaff : HeldProjectile
{
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.AmethystStaff);
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override void SetHostileDefaults()
    {
        Projectile.width = Projectile.height = ModUtils.Get_ItemTextureSize(ItemID.AmethystStaff).X;
        ExtraRotationValue = MathHelper.PiOver4;
        ShootProjectile = ProjectileID.AmethystBolt;
        shootVel = 5f;
        Projectile.damage = 50;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}
public class Held_TopazStaff : HeldProjectile
{
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.TopazStaff);
    public override void SetHostileDefaults()
    {
        Projectile.width = Projectile.height = ModUtils.Get_ItemTextureSize(ItemID.TopazStaff).X;
        ExtraRotationValue = MathHelper.PiOver4;
        ShootProjectile = ProjectileID.TopazBolt;
        shootVel = 5f; Projectile.damage = 51;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}
public class Held_SapphireStaff : HeldProjectile
{
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.SapphireStaff);
    public override void SetHostileDefaults()
    {
        Projectile.width = Projectile.height = ModUtils.Get_ItemTextureSize(ItemID.SapphireStaff).X;
        ExtraRotationValue = MathHelper.PiOver4;
        ShootProjectile = ProjectileID.SapphireBolt;
        shootVel = 7f; Projectile.damage = 52;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
        Projectile.timeLeft = 30;
    }
}
public class Held_EmeraldStaff : HeldProjectile
{
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.EmeraldStaff);
    public override void SetHostileDefaults()
    {
        Projectile.width = Projectile.height = ModUtils.Get_ItemTextureSize(ItemID.EmeraldStaff).X;
        ExtraRotationValue = MathHelper.PiOver4;
        ShootProjectile = ProjectileID.EmeraldBolt;
        shootVel = 7f; Projectile.damage = 53;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}
public class Held_RubyStaff : HeldProjectile
{
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.RubyStaff);
    public override void SetHostileDefaults()
    {
        Projectile.width = Projectile.height = ModUtils.Get_ItemTextureSize(ItemID.RubyStaff).X;
        ExtraRotationValue = MathHelper.PiOver4;
        ShootProjectile = ProjectileID.RubyBolt;
        shootVel = 9f; Projectile.damage = 54;
        Projectile.knockBack = 2;
        Projectile.timeLeft = 30;
    }
}
public class Held_DiamondStaff : HeldProjectile
{
    public override void Set_HeldProjStaticDefaults()
    {
        Mage = true;
    }
    public override string Texture => ModUtils.GetVanillaTexture<Item>(ItemID.DiamondStaff);
    public override void SetHostileDefaults()
    {
        Projectile.width = Projectile.height = ModUtils.Get_ItemTextureSize(ItemID.DiamondStaff).X;
        ExtraRotationValue = MathHelper.PiOver4;
        ShootProjectile = ProjectileID.DiamondBolt;
        shootVel = 9f; Projectile.damage = 55;
        Projectile.knockBack = 2; 
        Projectile.timeLeft = 30;
    }
}