using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModJam2.Content.NPCs
{
    public class NpcStaff : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_"+ItemID.DiamondStaff;
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
            Projectile.Center = holder.Center + Projectile.velocity;
            if (Projectile.timeLeft == 30)
            {
                var arrow = Projectile.NewProjectileDirect(null, Projectile.Center, Projectile.velocity.SafeNormalize(Vector2.UnitY) * 16, ProjectileID.DiamondBolt, 0, 0, -1);
                arrow.hostile = true;
                arrow.friendly = false;
    
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value,Projectile.Center - Main.screenPosition,null,lightColor,Projectile.velocity.ToRotation() + MathHelper.PiOver4,TextureAssets.Projectile[Type].Size() / 2f,1f,Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            return false;
        }
    }
}
