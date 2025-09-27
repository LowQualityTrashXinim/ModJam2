using Terraria;
using Terraria.ID;
using ModJam2.Texture;
using Terraria.ModLoader;
using ModJam2.Common.Utils;
namespace Roguelike.Contents.Items.Consumable.Scroll;
internal class MagicalScroll : ModItem
{
    public override string Texture => ModTexture.MissingTexture_Default;
    public override void SetDefaults()
    {
        Item.Item_DefaultToScroll(32, 32, ModContent.BuffType<MagicalSpell>(), ModUtils.ToMinute(2));
    }
}
public class MagicalSpell : ModBuff
{
    public override string Texture => ModTexture.EMPTYBUFF;
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = false;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<MagicalScrollPlayer>().MagicalScroll = true;
        player.manaCost += .05f;
    }
}
public class MagicalScrollPlayer : ModPlayer
{
    public bool MagicalScroll = false;
    public override void ResetEffects()
    {
        MagicalScroll = false;
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (MagicalScroll
            && proj.type != ProjectileID.MagicMissile
            && Main.rand.NextBool(5))
        {
            int damage = (int)Player.GetDamage(DamageClass.Magic).ApplyTo(42);
            Projectile.NewProjectile(Player.GetSource_ItemUse(Player.HeldItem), Player.Center, Main.rand.NextVector2CircularEdge(3, 3), ProjectileID.MagicMissile, damage, 4f, Player.whoAmI);
        }
    }
}