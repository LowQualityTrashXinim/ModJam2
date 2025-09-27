using Terraria;
using Terraria.ID;

namespace ModJam2.Common.Utils;
public static class ModUtils
{
    public static void Item_DefaultToScroll(this Item Item, int width, int height, int buffType, int buffTime)
    {
        Item.width = width; Item.height = height;
        Item.buffType = buffType;
        Item.buffTime = buffTime;
        Item.useTime = Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.HoldUp;
    }
    public static int ToSecond(int second) => 60 * second;
    public static int ToMinute(int minute) => 60 * 60 * minute;
}
