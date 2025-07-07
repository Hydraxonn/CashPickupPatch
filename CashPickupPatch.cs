using System;
using System.Reflection;
using GTA;
using GTA.Native;
public class CashPickupPatch : Script
{
    Random rng = new Random();
    int add = 0;
    public CashPickupPatch()
    {
        Tick += OnTick;
        Interval = 100;
        Function.Call(Hash.SET_HEALTH_SNACKS_CARRIED_BY_ALL_NEW_PEDS, 0.7f, 20);
    }
    private void OnTick(object sender, EventArgs e)//this is horrendously unoptimized, just a POC
    {
        //Function.Call(Hash.SET_HEALTH_SNACKS_CARRIED_BY_ALL_NEW_PEDS, 1.0f, 20);
        if (Game.Player.Character.Model.Hash != new Model("player_zero").Hash && Game.Player.Character.Model.Hash != new Model("player_one").Hash && Game.Player.Character.Model.Hash != new Model("player_two").Hash)//if not M F or T
        { 
            foreach (Prop pickup in World.GetAllPickupObjects())
            {
                if (pickup != null)
                {
                    float dist = Game.Player.Character.Position.DistanceTo(pickup.Position);
                    if (dist < 3)
                    {
                        switch (pickup.Model.Hash)
                        {
                            case -295781225: //prop_cash_pile_01
                                add = rng.Next(500, 2000);
                                //GTA.UI.Notification.Show("Pickup detected - Cash Pile");
                                Function.Call(Hash.PLAY_SOUND, -1, "PICK_UP_MONEY", "HUD_FRONTEND_CUSTOM_SOUNDSET");
                                Game.Player.Money += add;
                                pickup.Delete();
                                break;
                            case 880595258: //prop_ld_case_01
                                add = rng.Next(2000, 5000);
                                // GTA.UI.Notification.Show("Pickup detected - Briefcase");
                                Function.Call(Hash.PLAY_SOUND, -1, "PICK_UP_MONEY", "HUD_FRONTEND_CUSTOM_SOUNDSET");
                                Game.Player.Money += add;
                                pickup.Delete();
                                break;
                            case -1379254308: //Prop_LD_Wallet_01
                                add = rng.Next(500, 2000);
                                //GTA.UI.Notification.Show("Pickup detected - Wallet");
                                Function.Call(Hash.PLAY_SOUND, -1, "PICK_UP_MONEY", "HUD_FRONTEND_CUSTOM_SOUNDSET");
                                Game.Player.Money += add;
                                pickup.Delete();
                                break;
                            case -1666779307: //p_poly_bag_01_s
                                add = rng.Next(5000, 12000);
                                //GTA.UI.Notification.Show("Pickup detected - Cash Bag");
                                Function.Call(Hash.PLAY_SOUND, -1, "PICK_UP_MONEY", "HUD_FRONTEND_CUSTOM_SOUNDSET");
                                Game.Player.Money += add;
                                pickup.Delete();
                                break;
                            case -1249748547: //prop_security_case_01
                                add = rng.Next(7000, 20000);
                                //GTA.UI.Notification.Show("Pickup detected - Security Case");
                                Function.Call(Hash.PLAY_SOUND, -1, "PICK_UP_MONEY", "HUD_FRONTEND_CUSTOM_SOUNDSET");
                                Game.Player.Money += add;
                                pickup.Delete();
                                break;
                            case 289396019: //prop_money_bag_01
                                add = rng.Next(5000, 12000);
                                //GTA.UI.Notification.Show("Pickup detected - Money Bag");
                                Function.Call(Hash.PLAY_SOUND, -1, "PICK_UP_MONEY", "HUD_FRONTEND_CUSTOM_SOUNDSET");
                                Game.Player.Money += add;
                                pickup.Delete();
                                break;
                            case 1374501775: //prop_choc_pq
                                Function.Call(Hash.PLAY_SOUND, -1, "PICK_UP_SNACK", "HUD_FRONTEND_CUSTOM_SOUNDSET");
                                AddHealthPickup(1);
                                pickup.Delete();
                                break;
                        }
                    }
                }
            }
        }
    }
    public static void AddHealthPickup(int Amt)
    {
        Assembly assembly = Assembly.LoadFrom("scripts\\SinglePlayerHeists.dll");
        Type type = assembly.GetType("SinglePlayerHeists.UtilsMisc.LesterContact");
        MethodInfo method = type.GetMethod("AddHealth");
        object[] parameters = new object[1] { Amt };
        method.Invoke(method, parameters);
    }
}