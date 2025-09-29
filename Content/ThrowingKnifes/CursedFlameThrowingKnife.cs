using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModJam2.Common.Utils;
using ModJam2.Texture;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModJam2.Content.ThrowingKnifes
{
    internal class CursedFlameThrowingKnife : BaseThrowingKnife
    {
        public override void Knife_SetDefaults(out int damage, out int usespeed, out int shoot, out float speed)
        {
            base.Knife_SetDefaults(out damage, out usespeed, out shoot, out speed);
            damage = 150;
            shoot = ModContent.ProjectileType<CursedFlameThrowingKnife_Projectile>();
            speed = 3;
        }
    }
    public class CursedFlameThrowingKnife_Projectile : ModProjectile
    {
        public override string Texture => ModTexture.GetTheSameTextureAsEntity<CursedFlameThrowingKnife>();
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ThrowingKnife);
            Projectile.aiStyle = -1;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 9999;
            Projectile.extraUpdates = 5;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Main.rand.NextBool())
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.CursedTorch);
                dust.noGravity = true;
                dust.velocity = Vector2.Zero;
                dust.position += Main.rand.NextVector2Circular(10, 10);
            }
            if (Projectile.ai[1] >= 90)
            {
                Projectile.velocity.Y += .25f;
                if (Projectile.velocity.Y >= 16)
                {
                    Projectile.velocity.Y = 16;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, ModUtils.ToMinute(1));
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = texture.Size() * .5f;
            Vector2 drawpos = Projectile.Center - Main.screenPosition;
            Main.EntitySpriteDraw(texture, drawpos, null, lightColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None);
            return false;
        }
    }
}


