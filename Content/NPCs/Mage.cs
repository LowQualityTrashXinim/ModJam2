using Microsoft.Xna.Framework;
using ModJam2.Common.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModJam2.Content.NPCs
{
    public class Mage : ModNPC
    {
        const int CopyNPC = NPCID.Tim;
        int playerUnreachableDuration = 0;
        const int TP_COOLDOWN = 120;
        int RandomizeHeldItem = 0;
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Tim;

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
            AnimationType = CopyNPC;
            NPC.aiStyle = -1;
            NPC.alpha = 255;
        }
        public override void OnSpawn(IEntitySource source)
        {
            RandomizeHeldItem = Main.rand.Next(ModJam2System.Mage_HeldProjectile);
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
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 0, 0, DustID.Cloud);
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
            NPC.TargetClosest();

            Vector2 tpPos = NPC.targetRect.Center() + Vector2.UnitX * 128 * NPC.direction;
            NPC.velocity.X = 0;

            if (NPC.targetRect.Center().Y < NPC.Center.Y - 64)
                playerUnreachableDuration = (int)MathHelper.Clamp(playerUnreachableDuration + 1, 0, TP_COOLDOWN);
            else
                playerUnreachableDuration = (int)MathHelper.Clamp(playerUnreachableDuration - 1, 0, TP_COOLDOWN);

            if (playerUnreachableDuration >= TP_COOLDOWN / 2)
                for (int i = 0; i < 15; i++)
                    Dust.NewDustDirect(tpPos, 32, 64, DustID.RuneWizard, 0, 0, 0, Color.Gray, 1).noGravity = true;

            if (playerUnreachableDuration >= TP_COOLDOWN)
            {
                NPC.Center = tpPos;
                playerUnreachableDuration = 0;
                NPC.TargetClosest();
                for (int i = 0; i < 64; i++)
                    Dust.NewDustPerfect(NPC.Center, DustID.RuneWizard, Main.rand.NextVector2CircularEdge(16, 16)).noGravity = true;

            }
            NPC.localAI[2] = (int)MathHelper.Clamp(NPC.localAI[2] - 1, 0, TP_COOLDOWN);

            if (NPC.localAI[2] == 0 && NPC.Distance(NPC.targetRect.Center()) <= 725)
            {
                ModUtils.SpawnHostileHeldItem(NPC.GetSource_FromAI(), NPC, NPC.Center.DirectionTo(NPC.targetRect.Center()), RandomizeHeldItem);
                NPC.localAI[2] = 120;
            }

        }

    }
}
