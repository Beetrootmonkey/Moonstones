using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Moonstones.Items
{
	public class MoonstoneRanged : ModItem
	{
    
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Rightclick to apply its modifier to your held weapon.");
		}

		public override void SetDefaults()
		{
			item.height = 32;
			item.width = 32;
		
			item.damage = 5;
			item.knockBack = 5;
			//item.mana = 5; -- You dont need to assign mana if it is a melee weapon so i took this out
		
			item.value = 10000;
			item.rare = 2;
			item.maxStack = 1;
			item.SetNameOverride("Moonstone (Ranged)");
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
		
			/*
			I changed this so it would be impossible to get a non-ranged prefix
			*/
		
			int roll = rand.Next(37);
			byte pfix;
			
			if (roll <= 10)
			{
				pfix = (byte)rand.Next(16, 25);
			}
			else if (roll <= 26)
			{
				pfix = (byte)rand.Next(36, 51);
			}
			else if (roll <= 35)
			{
				pfix = (byte)rand.Next(53, 61);
			}
			else
			{
				pfix = (byte)82;
				item.rare = 10;//This is to have the reforged moonstone show up bright purple if Unreal so it will contrast the other colors

			}
			
			return pfix;//This is to have the reforged moonstone show up bright purple if Legendary so it will contrast the other colors
			}
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
