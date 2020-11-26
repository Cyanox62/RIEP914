using Exiled.Events.EventArgs;
using Exiled.API.Features;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace RIEP914
{
	class EventHandlers
	{
		private List<RoleType> scps = new List<RoleType>()
		{
			RoleType.Scp049,
			RoleType.Scp096,
			RoleType.Scp106,
			RoleType.Scp173,
			RoleType.Scp93953,
		};

		private void ChangeRole(Player player, RoleType newRole)
		{
			player.SetRole(newRole, true);
			player.Inventory.items.Clear();
			foreach (ItemType id in player.GameObject.GetComponent<CharacterClassManager>()?.Classes.SafeGet((int)newRole).startItems)
			{
				player.Inventory.AddNewItem(id);
			}
		}

		internal void OnChangeRole(ChangingRoleEventArgs ev)
		{
			if (ev.NewRole == RoleType.ClassD)
			{
				ev.Items.Add(ItemType.Coin);
			}
		}

		internal void OnUpgradingItems(UpgradingItemsEventArgs ev)
		{
			if (ev.KnobSetting == Scp914.Scp914Knob.Coarse)
			{
				foreach (Player player in ev.Players)
				{
					Vector3 pos = Map.Rooms[UnityEngine.Random.Range(0, Map.Rooms.Count)].Position;
					pos.y += 2f;
					Timing.CallDelayed(0.1f, () => player.Position = pos);
					player.Health *= REP914.singleton.Config.courceHealthPercent / 100f;
				}
			}
			else if (ev.KnobSetting == Scp914.Scp914Knob.OneToOne)
			{
				foreach (Player player in ev.Players)
				{
					if (player.Team == Team.SCP && player.Role != RoleType.Scp0492)
					{
						List<RoleType> scpList = scps.Where(x => x != player.Role).ToList();
						ChangeRole(player, scpList[UnityEngine.Random.Range(0, scpList.Count)]);
					}
					else if (player.Team == Team.CDP)
					{
						ChangeRole(player, RoleType.Scientist);
					}
					else if (player.Team == Team.RSC)
					{
						ChangeRole(player, RoleType.ClassD);
						player.Inventory.AddNewItem(ItemType.Coin);
					}
					else if (player.Team == Team.MTF)
					{
						ChangeRole(player, RoleType.ChaosInsurgency);
					}
					else if (player.Team == Team.CHI)
					{
						ChangeRole(player, (RoleType)REP914.singleton.Config.mtfClassId);
					}
				}
			}
			else if (ev.KnobSetting == Scp914.Scp914Knob.Fine)
			{
				foreach (Player player in ev.Players)
				{
					Log.Info(player.CurrentItem.id);
					if (player.CurrentItem.id == ItemType.Coin)
					{
						Log.Info("swapping");
						player.Inventory.items.Remove(player.CurrentItem);
						player.Inventory.AddNewItem(ItemType.KeycardJanitor);
					}
				}
			}
			else if (ev.KnobSetting == Scp914.Scp914Knob.VeryFine)
			{
				foreach (Player player in ev.Players)
				{
					if (player.Role == RoleType.Scp0492)
					{
						if (UnityEngine.Random.Range(0, 101) <= REP914.singleton.Config.zombieToClassdPercent)
						{
							ChangeRole(player, RoleType.ClassD);
						}
					}
				}
			}
		}
	}
}
