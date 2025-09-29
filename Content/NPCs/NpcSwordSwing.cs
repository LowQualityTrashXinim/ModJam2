using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ModJam2.Content.NPCs
{
    public class NpcSwordSwing : ModProjectile
    {

        public override string Texture => "Terraria/Images/Item_4";
        public override void SetDefaults()
        {
            Projectile.timeLeft = 60;
            Projectile.width = Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            NPC holder = Main.npc[(int)Projectile.ai[0]];
            holder.velocity = Vector2.Zero;
            float percentDone = Projectile.timeLeft <= 45 ? Projectile.timeLeft / 45f : 1;
		    Projectile.direction = holder.direction;
		    float baseAngle = holder.direction == 1 ? MathHelper.PiOver4 - MathHelper.PiOver4 / 4 : MathHelper.PiOver2 + (MathHelper.PiOver4 + MathHelper.PiOver4 / 4);
		    float angle = MathHelper.ToRadians(135) * Projectile.direction;
		    float start = baseAngle + angle;
		    float end = baseAngle - angle;
		    float rotation = MathHelper.Lerp(start, end, percentDone);
		    Projectile.rotation = rotation + MathHelper.PiOver4;
		    Projectile.velocity.X = Projectile.direction;
		    Projectile.Center = holder.Center + Vector2.UnitX.RotatedBy(rotation) * 35f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value,Projectile.Center - Main.screenPosition,null,lightColor,Projectile.rotation,TextureAssets.Projectile[Type].Size() / 2f,1f,Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            return false;
        }

    }
}
