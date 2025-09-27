using Terraria;
using Terraria.ModLoader;
using ModJam2.Texture;
using ModJam2.Common.Utils;

namespace Roguelike.Contents.Items.Consumable.Scroll;
internal class ScrollOfInvincibility : ModItem
{
    public override string Texture => ModTexture.MissingTexture_Default;
    public override void SetDefaults()
    {
        Item.Item_DefaultToScroll(32, 32, ModContent.BuffType<InvincibilitySpell>(), ModUtils.ToSecond(10));
    }
}
public class InvincibilitySpell : ModBuff
{
    public override string Texture => ModTexture.EMPTYBUFF;
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = false;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.noFallDmg = true;
    }
}

public class ScrollOfInvincibilityPlayer : ModPlayer
{
    public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
    {
        if (Player.HasBuff(ModContent.BuffType<InvincibilitySpell>()))
        {
            return false;
        }
        return base.CanBeHitByNPC(npc, ref cooldownSlot);
    }
    public override bool CanBeHitByProjectile(Projectile proj)
    {
        if (Player.HasBuff(ModContent.BuffType<InvincibilitySpell>()))
        {
            return false;
        }
        return base.CanBeHitByProjectile(proj);
    }
}
