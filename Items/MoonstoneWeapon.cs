using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Moonstones.Items
{
	public class MoonstoneWeapon : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Rightclick to apply its modifier to your held weapon.");
		}

		public override void SetDefaults()
		{
			item.value = 10000;
			item.rare = 2;
            item.maxStack = 1;
            item.SetNameOverride("Moonstone (Weapons)");
        }



        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return (byte)rand.Next(1, 62);
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            Item item = player.HeldItem;
            if (item != null && item.damage > 0)
            {
                item.prefix = this.item.prefix;
            }
            else
            {
                int number = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, this.item.type, 1, false, this.item.prefix, false, false);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }
}