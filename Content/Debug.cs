using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModJam2.Common.Subworlds;
using ModJam2.Texture;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ModJam2.Content;

internal class TestEnterSubWorld : ModItem
{
    public override string Texture => ModTexture.Scroll;
    public override void SetDefaults()
    {
        Item.consumable = false;
        Item.width = Item.height = 32;
        Item.useAnimation = Item.useTime = 20;
        Item.useStyle = ItemUseStyleID.HoldUp;
    }
    public override bool? UseItem(Player player)
    {
        if (SubworldSystem.Current == null && player.ItemAnimationJustStarted)
            SubworldSystem.Enter<CursedKingdomSubworld>();
        return true;
    }
}
public class PortalTileEntity : ModTileEntity
{
    public override bool IsTileValidForEntity(int x, int y)
    {
        Tile tile = Main.tile[x, y];
        return tile.HasTile && tile.TileType == ModContent.TileType<Portal>();
    }
}
public class Portal : ModTile
{
    public override void SetStaticDefaults()
    {
        TileID.Sets.PreventsTileRemovalIfOnTopOfIt[Type] = true;
        TileID.Sets.PreventsTileHammeringIfOnTopOfIt[Type] = true;
        TileID.Sets.AvoidedByMeteorLanding[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = false;
        Main.tileFrame[Type] = 30;
        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(238, 145, 105), name);
        // Placement
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.HookPostPlaceMyPlayer = ModContent.GetInstance<PortalTileEntity>().Generic_HookPostPlaceMyPlayer;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.addTile(Type);
    }
    public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
    {
        if (i % 2 == 1)
            spriteEffects = SpriteEffects.FlipHorizontally;
    }
    public override bool CanKillTile(int i, int j, ref bool blockDamaged)
    {
        return false;
    }
    public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
    {
        int uniqueAnimationFrame = Main.tileFrame[Type] + i;
        if (i % 2 == 0)
            uniqueAnimationFrame += 3;
        if (i % 3 == 0)
            uniqueAnimationFrame += 3;
        if (i % 4 == 0)
            uniqueAnimationFrame += 3;
        uniqueAnimationFrame %= 6;
    }
    public override void AnimateTile(ref int frame, ref int frameCounter)
    {

        if (++frameCounter >= 9)
        {
            frameCounter = 0;
            frame = ++frame % 30;
        }
    }
    public override bool RightClick(int i, int j)
    {
        if (SubworldSystem.Current == null)
            SubworldSystem.Enter<CursedKingdomSubworld>();
        return Main.hardMode;
    }
}