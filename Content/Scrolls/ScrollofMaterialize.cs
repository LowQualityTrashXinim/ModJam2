using Terraria;
using Terraria.ID;
using ModJam2.Texture;
using Terraria.ModLoader;
using ModJam2.Common.Utils;

namespace Roguelike.Contents.Items.Consumable.Scroll;
internal class ScrollOfMaterialize : ModItem
{
    public override string Texture => ModTexture.Scroll;
    public override void SetDefaults()
    {
        Item.Item_DefaultToScroll(32, 32, ModContent.BuffType<MaterializeSpell>(), ModUtils.ToMinute(4));
    }
}
public class MaterializeSpell : ModBuff
{
    public override string Texture => ModTexture.EMPTYBUFF;
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = false;
        Main.buffNoSave[Type] = true;
    }
}
public class ScrollOfMaterializePlayer : ModPlayer
{
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Player.HasBuff<MaterializeSpell>())
        {
            if (Main.rand.NextBool(15))
            {
                Item.NewItem(Player.GetSource_OnHit(target), target.Hitbox, Main.rand.NextBool() ? ItemID.Heart : ItemID.Star);
            }
        }
    }
}

