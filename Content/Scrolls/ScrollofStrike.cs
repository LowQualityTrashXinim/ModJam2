using Terraria;
using Terraria.ModLoader;
using ModJam2.Texture;
using ModJam2.Common.Utils;

namespace Roguelike.Contents.Items.Consumable.Scroll
{
    class ScrollofStrike : ModItem
    {
        public override string Texture => ModTexture.MissingTexture_Default;
        public override void SetDefaults()
        {
            Item.Item_DefaultToScroll(32, 32, ModContent.BuffType<StrikeSpell>(), ModUtils.ToMinute(1));
        }
    }
    public class StrikeSpell : ModBuff
    {
        public override string Texture => ModTexture.EMPTYBUFF;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += .1f;
        }
    }
    public class StrikePlayer : ModPlayer
    {
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.HasBuff<StrikeSpell>())
            {
                Player.DelBuff(Player.FindBuffIndex(ModContent.BuffType<StrikeSpell>()));
                for (int i = 0; i < 5; i++)
                {
                    Player.StrikeNPCDirect(target, hit);
                }
            }
        }
    }
}
