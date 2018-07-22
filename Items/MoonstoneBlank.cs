using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Moonstones.Items
{
	public class MoonstoneBlank : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Rightclick to awaken.");
		}

		public override void SetDefaults()
        	{
            		item.value = 10000;
			item.rare = 2;
            		item.maxStack = 9999;
            		item.SetNameOverride("Moonstone (Dormant)");
        	}

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            ModItem item = null;
            if (Main.rand.Next(2) == 1)
            {
                item = mod.GetItem("MoonstoneWeapon");
            } else
            {
                item = mod.GetItem("MoonstoneAccessory");
            }
            int prefix = item.ChoosePrefix(Main.rand);
            int number = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item.item.type, 1, false, prefix, false, false);
            if (Main.netMode == 1)
            {
                NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
            }
        }
	}
}
