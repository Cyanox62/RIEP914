using Exiled.API.Interfaces;
using System.ComponentModel;

namespace RIEP914
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		[Description("Determines what percent of the player's health they'll have left after going through SCP-914 on Course.")]
		public int courceHealthPercent { get; set; } = 50;
		[Description("Determines the percent chance of a zombie becoming a Class-D when put through SCP-914 on Very Fine.")]
		public int zombieToClassdPercent { get; set; } = 50;

		[Description("Determines the MTF class to set a player as when they switch from Chaos to MTF in SCP-914. Cadet = 13, Lieutenant = 11, Commander = 12.")]
		public int mtfClassId { get; set; } = 11;
	}
}
