using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
    public class NpcGrappleHook : ModProjectile
    {

        ref float ownerNPC => ref Projectile.ai[0];
        Vector2 prePullNpcPos = Vector2.Zero;
        bool isPulling = false;
        ref NPC npc => ref Main.npc[(int)ownerNPC];
        ref float pullProgress => ref Projectile.ai[1];
        bool returning = false;
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Hook;
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.width = 16;
            Projectile.height = 16;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            isPulling = true;
            prePullNpcPos = npc.Center;
            Projectile.velocity = Vector2.Zero;
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }

        public override void AI()
        {
            if (!npc.active)
                Projectile.Kill();

            Projectile.rotation = Projectile.DirectionFrom(npc.Center).ToRotation() + MathHelper.PiOver2; 

            if(Projectile.timeLeft <= 3560 && !isPulling)
            {
                Projectile.velocity = Projectile.DirectionTo(npc.Center) * 14;
                if(Projectile.Distance(npc.Center) <= 16)
                    Projectile.Kill();
            }    

            if (isPulling)
            {
                pullProgress++;
                npc.Center = Vector2.Lerp(prePullNpcPos, Projectile.Center, pullProgress / 20);
                if (pullProgress == 20)
                {

                    npc.velocity = new Vector2(0, -8);
                    Projectile.Kill();

                }
            } else if(WorldGen.TileType(Projectile.Center.ToTileCoordinates().X,Projectile.Center.ToTileCoordinates().Y) == TileID.Platforms)
            {
                OnTileCollide(Projectile.oldVelocity);
            }
        }
        public override bool PreDrawExtras()
        {
            Asset<Texture2D> chainTexture = TextureAssets.Chain;
            Vector2 npcCenter = npc.Center;
            Vector2 center = Projectile.Center;
            Vector2 directionToNPC = npcCenter - Projectile.Center;
            float chainRotation = directionToNPC.ToRotation() - MathHelper.PiOver2;
            float distanceToNPC = directionToNPC.Length();

            while (distanceToNPC > 20f && !float.IsNaN(distanceToNPC))
            {
                directionToNPC /= distanceToNPC; 
                directionToNPC *= chainTexture.Height();
                center += directionToNPC;
                directionToNPC = npcCenter - center; 
                distanceToNPC = directionToNPC.Length();
                Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));
                Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
                    chainTexture.Value.Bounds, drawColor, chainRotation,
                    chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            }
            return false;
        }
    }
}
