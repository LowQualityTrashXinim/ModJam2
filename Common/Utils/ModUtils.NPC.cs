using Microsoft.Xna.Framework;
using ModJam2.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;

namespace ModJam2.Common.Utils
{
    public static partial class ModUtils
    {
        public static Projectile SpawnHostileHeldItem(IEntitySource source, NPC whoAmI, Vector2 vel, int type)
        {
            Projectile proj = Projectile.NewProjectileDirect(source, whoAmI.Center, vel, type, 0, 0);
            if (proj.ModProjectile is BaseHostileProjectile hostile)
            {
                hostile.SetNPCOwner(whoAmI.whoAmI);
                hostile.CanDealContactDamage = false;
            }
            return proj;
        }
        public static int NewHostileProjectile(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int whoAmI = -1, bool AdjustHostileProjectileDamage = true)
        {
            if (AdjustHostileProjectileDamage)
            {
                if (Main.expertMode)
                    damage /= 4;
                else if (Main.masterMode)
                    damage /= 6;
                else
                    damage /= 2;
            }

            if (damage < 1)
            {
                damage = 1;
            }
            int HostileProjectile = Projectile.NewProjectile(source, position, velocity, type, damage, knockback);

            Main.projectile[HostileProjectile].whoAmI = whoAmI;
            Main.projectile[HostileProjectile].hostile = true;
            Main.projectile[HostileProjectile].friendly = false;
            return HostileProjectile;
        }
        public static void Heal(this NPC npc, int healAmount, bool texteffect = true)
        {
            int simulatehealing = npc.life + healAmount;
            if (npc.lifeMax <= simulatehealing)
            {
                npc.life = npc.lifeMax;
            }
            else
            {
                npc.life = simulatehealing;
            }
            if (texteffect)
            {
                CombatText.NewText(npc.Hitbox, CombatText.HealLife, healAmount);
            }
        }
    }
}
