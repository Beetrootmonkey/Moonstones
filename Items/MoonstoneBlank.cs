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
			item.height = 32;
			item.width = 32;
			
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
		
			/*
			So I wanted to add a weapon moonstone for each damage pool type
			
			Accessory for Accessories
			Melee for Melee damage
			Ranged for Ranged and Thrown damage
			Arcane for Magic and Summoning damage
			
			This would allow you to make the mod compatible with mods that add new damage types
			*/
		
			int roll = Main.rand.Next(4);
			ModItem item = null;
			if (roll == 1)
			{
				item = mod.GetItem("MoonstoneAccessory");
			}
			else if (roll == 2)
			{
				item = mod.GetItem("MoonstoneMelee");
			}
			else if (roll == 3)
			{
				item = mod.GetItem("MoonstoneRanged");
			}
			else
			{
				item = mod.GetItem("MoonstoneArcane");
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
