using Terraria;
using Terraria.ModLoader;
using ModJam2.Texture;
using ModJam2.Common.Utils;
namespace Roguelike.Contents.Items.Consumable.Scroll;
internal class ScrollofEvasive : ModItem
{

    public override string Texture => ModTexture.MissingTexture_Default;
    public override void SetDefaults()
    {
        Item.Item_DefaultToScroll(32, 32, ModContent.BuffType<EvasionSpell>(), ModUtils.ToSecond(20));
    }
}
public class EvasionSpell : ModBuff
{
    public override string Texture => ModTexture.EMPTYBUFF;
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = false;
        Main.buffNoSave[Type] = true;
    }
}
public class ScrollOfEnvasionPlayer : ModPlayer
{
    public override bool FreeDodge(Player.HurtInfo info)
    {
        if (Player.HasBuff(ModContent.BuffType<EvasionSpell>()))
        {
            Player.DelBuff(Player.FindBuffIndex(ModContent.BuffType<EvasionSpell>()));
            Player.AddImmuneTime(info.CooldownCounter, 60);
            return true;
        }
        return base.FreeDodge(info);
    }
}
