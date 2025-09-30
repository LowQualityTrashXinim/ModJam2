using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ModJam2.Content.NPCs
{
    public class Knight : ModNPC
    {

        const int CopyNPC = NPCID.ArmoredSkeleton;
        int playerUnreachableDuration = 0;
        const int GRAPPLE_COOLDOWN = 120;
        NpcGrappleHook hook = null;
        NpcSwordSwing sword = null;
        public override string Texture => "Terraria/Images/NPC_" + CopyNPC;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[CopyNPC];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(CopyNPC);
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.noGravity = false;
            NPC.GravityMultiplier *= 1.1f;
            AIType = -1;
            AnimationType = CopyNPC;
            NPC.alpha = 255;

        }
        int SpawningAnimationTime = 300;
        public override bool PreAI()
        {
            if (--SpawningAnimationTime >= 0)
            {
                if (SpawningAnimationTime <= 255)
                {
                    NPC.alpha--;
                }
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Cloud);
                    dust.noGravity = true;
                    dust.velocity = Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(1, 3);
                    dust.rotation = MathHelper.ToRadians(Main.rand.NextFloat(90));
                    dust.scale += Main.rand.NextFloat(.5f);
                    NPC.velocity = Vector2.Zero;
                }
                return false;
            }
            return base.PreAI();
        }

        public override void AI()
        {

            if (NPC.targetRect.Center().Y < NPC.Center.Y - 64)
                playerUnreachableDuration = (int)MathHelper.Clamp(playerUnreachableDuration + 1, 0, GRAPPLE_COOLDOWN);
            else
                playerUnreachableDuration = (int)MathHelper.Clamp(playerUnreachableDuration - 1, 0, GRAPPLE_COOLDOWN);
            if (playerUnreachableDuration >= GRAPPLE_COOLDOWN && !Collision.CanHitWithCheck(NPC.Center, 16, 16, NPC.targetRect.Center(), 16, 16, (x, y) => { return WorldGen.TileType(x, y) != TileID.Platforms; }))
            {
                hook = Projectile.NewProjectileDirect(null, NPC.Center, NPC.DirectionTo(NPC.targetRect.Center()) * 15, ModContent.ProjectileType<NpcGrappleHook>(), 0, 0, -1, NPC.whoAmI).ModProjectile as NpcGrappleHook;
                playerUnreachableDuration = 0;
                NPC.TargetClosest();
            }
            NPC.localAI[2] = (int)MathHelper.Clamp(NPC.localAI[2] - 1, 0, GRAPPLE_COOLDOWN);

            if (NPC.localAI[2] == 0 && NPC.Distance(NPC.targetRect.Center()) <= 32)
            {
                Projectile.NewProjectileDirect(null, NPC.Center, Vector2.Zero, ModContent.ProjectileType<NpcSwordSwing>(), 0, 0, -1, NPC.whoAmI);
                NPC.localAI[2] = 30;
            }

        }

    }
}
