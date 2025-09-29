using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModJam2.Content.NPCs
{
    public class Archer : ModNPC
    {
         const int CopyNPC = NPCID.ArmoredViking;
        int playerUnreachableDuration = 0;
        const int GRAPPLE_COOLDOWN = 120;
        NpcGrappleHook hook = null;
        public override string Texture => "Terraria/Images/NPC_"+NPCID.ArmoredViking;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[CopyNPC];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers() { 
				Velocity = 1f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(CopyNPC);
            AnimationType = CopyNPC;
            
        }

        public override void AI()
        {
    
            if(NPC.targetRect.Center().Y < NPC.Center.Y - 64)
                playerUnreachableDuration = (int)MathHelper.Clamp(playerUnreachableDuration + 1,0,GRAPPLE_COOLDOWN);
            else
                playerUnreachableDuration = (int)MathHelper.Clamp(playerUnreachableDuration - 1,0,GRAPPLE_COOLDOWN);
            if(playerUnreachableDuration >= GRAPPLE_COOLDOWN && !Collision.CanHitWithCheck(NPC.Center,16,16,NPC.targetRect.Center(),16,16,(x,y) => { return WorldGen.TileType(x,y) != TileID.Platforms; }))
            {
                hook = Projectile.NewProjectileDirect(null,NPC.Center,NPC.DirectionTo(NPC.targetRect.Center()) * 15,ModContent.ProjectileType<NpcGrappleHook>(),0,0,-1,NPC.whoAmI).ModProjectile as NpcGrappleHook;
                playerUnreachableDuration = 0;
                NPC.TargetClosest();
            }
            NPC.localAI[2] = (int)MathHelper.Clamp(NPC.localAI[2] - 1,0,GRAPPLE_COOLDOWN);

            if(NPC.localAI[2] == 0 && NPC.Distance(NPC.targetRect.Center()) <= 725)
            {
                Projectile.NewProjectileDirect(null,NPC.Center,NPC.DirectionTo(NPC.targetRect.Center()) * 16,ModContent.ProjectileType<NpcBow>(),0,0,-1,NPC.whoAmI);
                NPC.localAI[2] = 120;
            }

        }

    }
    
}
