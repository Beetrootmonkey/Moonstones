using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Moonstones.Items
{
	public class MoonstoneAccessory: ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Rightclick to apply its modifier to your held accessory.");
		}

		public override void SetDefaults()
		{
			//Making the item size bigger because I expirienced a glitch where it would fall into blocks.
			item.height = 32;
			item.width = 32;
		
			item.value = 10000;
			item.rare = 2;
			item.maxStack = 1;
			item.SetNameOverride("Moonstone");
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
		
			/*
			So I changed this entire block in order to have a feature to change the reforge color-
			bright purple if the prefix was the 'best' possible one for that item type.
			*/
		
			int[] best = {65, 66, 68, 72, 76, 80}; //The prefix values for the 'best' prefixes on any accessory.
		
			byte pfix = (byte)rand.Next(62, 80);//Changed this to 80 because 81-84 are reserved for non accessory prefixes 
			
			foreach (int x in best)
			{
				if ((int)pfix == x)
				{
					item.rare = 10;
				}
			}
			return pfix;
		}

		public override bool CanRightClick()
		{
		return true;
		}

		public override void RightClick(Player player)
		{
			Item item = player.HeldItem;
			if (item != null && item.accessory)
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
