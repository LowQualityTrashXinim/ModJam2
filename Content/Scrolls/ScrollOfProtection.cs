using Terraria;
using Terraria.ModLoader;
using ModJam2.Texture;
using ModJam2.Common.Utils;

namespace ModJam2.Content.Scrolls;
internal class ScrollOfProtection : ModItem
{
    public override string Texture => ModTexture.Scroll;
    public override void SetDefaults()
    {
        Item.Item_DefaultToScroll(32, 32, ModContent.BuffType<ProtectionSpell>(), ModUtils.ToSecond(20));
    }
}
public class ProtectionSpell : ModBuff
{
    public override string Texture => ModTexture.EMPTYBUFF;
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = false;
        Main.buffNoSave[Type] = true;
    }
}
public class ProtectionSpell_Player : ModPlayer
{
    public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
        if (Player.HasBuff<ProtectionSpell>())
        {
            modifiers.SetMaxDamage(1);
            modifiers.Knockback *= 0;
            Player.DelBuff(Player.FindBuffIndex(ModContent.BuffType<ProtectionSpell>()));
        }
    }
    public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
        if (Player.HasBuff<ProtectionSpell>())
        {
            modifiers.SetMaxDamage(1);
            modifiers.Knockback *= 0;
            Player.DelBuff(Player.FindBuffIndex(ModContent.BuffType<ProtectionSpell>()));
        }
    }
}

