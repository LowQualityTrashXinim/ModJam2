using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModJam2.Common.Utils;
using ModJam2.Texture;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModJam2.Content.Projectiles;
public abstract class BaseHostileProjectile : ModProjectile
{
    public sealed override void SetDefaults()
    {
        Projectile.hostile = true;
        Projectile.friendly = false;
        Projectile.tileCollide = false;
        SetHostileDefaults();
        InitialTimeLeft = Projectile.timeLeft;
    }
    protected int InitialTimeLeft = 0;
    public virtual void SetHostileDefaults() { }
    public override string Texture => ModTexture.MissingTexture_Default;
    public int ItemIDtextureValue = 1;
    int NPC_WhoAmI = -1;
    public bool CanDealContactDamage = true;
    protected bool NaturalDieOff = true;
    public override bool CanHitPlayer(Player target)
    {
        return CanDealContactDamage;
    }
    public bool IsNPCActive(out NPC npc)
    {
        npc = null;
        if (NPC_WhoAmI < 0 || NPC_WhoAmI > 255)
        {
            return false;
        }
        npc = Main.npc[NPC_WhoAmI];
        if (npc.active && npc.life > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetNPCOwner(int whoAmI)
    {
        NPC_WhoAmI = whoAmI;
    }
    public virtual void PreDrawDraw(Texture2D texture, Vector2 drawPos, Vector2 origin, ref Color lightColor, out bool DrawOrigin) { DrawOrigin = true; }
    public override bool PreDraw(ref Color lightColor)
    {
        Main.instance.LoadProjectile(Type);
        Texture2D texture = TextureAssets.Projectile[Type].Value;
        Vector2 origin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
        Vector2 drawPos = Projectile.position - Main.screenPosition + origin + new Vector2(0f, Projectile.gfxOffY);
        SpriteEffects effect = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;
        PreDrawDraw(texture, drawPos, origin, ref lightColor, out bool DrawOrigin);
        if (DrawOrigin)
        {
            Main.EntitySpriteDraw(texture, drawPos, null, lightColor, Projectile.rotation, origin, Projectile.scale, effect, 0);
        }
        return false;
    }
    public sealed override void OnKill(int timeLeft)
    {
        if (NaturalDieOff)
        {
            return;
        }
        for (int i = 0; i < 10; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Smoke);
            dust.noGravity = true;
            dust.velocity = Main.rand.NextVector2CircularEdge(3, 3) * Main.rand.NextFloat(.75f, 1.25f);
            dust.scale = Main.rand.NextFloat(2, 3.5f);
        }
    }
}
public abstract class HeldProjectile : BaseHostileProjectile
{
    /// <summary>
    /// This is for Mage and Archer projectile randomize chooser
    /// </summary>
    public bool Mage = false;
    public virtual void Set_HeldProjStaticDefaults() { }
    public override sealed void SetStaticDefaults()
    {
        Set_HeldProjStaticDefaults();
        if (Mage)
        {
            ModJam2System.Mage_HeldProjectile.Add(Type);
        }
        else
        {

        }
    }
    public int ShootProjectile = 0;
    public float shootVel = 1;
    public float ExtraRotationValue = 0;
    public virtual void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        ModUtils.NewHostileProjectile(source, position, velocity, type, damage, knockback, AdjustHostileProjectileDamage: false);
    }
    public override void AI()
    {
        if (Projectile.timeLeft == InitialTimeLeft)
        {
            Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);
            Projectile.rotation = Projectile.velocity.ToRotation() + ExtraRotationValue;
            if (ShootProjectile != ProjectileID.None)
            {
                Shoot(Projectile.GetSource_FromAI(), Projectile.Center + Projectile.velocity * Projectile.Size.Length() * .7f, Projectile.velocity.SafeNormalize(Vector2.Zero) * shootVel, ShootProjectile, Projectile.damage, Projectile.knockBack);
            }
        }
        if (IsNPCActive(out NPC npc))
        {
            Projectile.Center = npc.Center + Projectile.velocity * Projectile.Size.Length() * .5f;
        }
        else
        {
            NaturalDieOff = false;
            Projectile.Kill();
        }
    }
}
