//If you haven't done so yet, run the setup.bat file in your project/libs folder to acquire the needed references.
using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;

namespace AccelItemMod
{
    //This is an example plugin that can be put in BepInEx/plugins/ExamplePlugin/ExamplePlugin.dll to test out.
    //It's a small plugin that adds a relatively simple item to the game, and gives you that item whenever you press F2.

    //This attribute specifies that we have a dependency on R2API, as we're using it to add our item to the game.
    //You don't need this if you're not using R2API in your plugin, it's just to tell BepInEx to initialize R2API before this plugin so it's safe to use R2API.
    [BepInDependency("com.bepis.r2api")]

    //This attribute is required, and lists metadata for your plugin.
    [BepInPlugin(
        //The GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config). Java package notation is commonly used, which is "com.[your name here].[your plugin name here]"
        "com.Flizz95.AccelItemMod",
        //The name is the name of the plugin that's displayed on load
        "Acceleration Item Mod",
        //The version number just specifies what version the plugin is.
        "0.1.0")]
    //Like seriously, if we see this boilerplate on thunderstore, we will deprecate this mod. Change that name!
    //If you want to test package uploading in general, try using beta.thunderstore.io

    //We will be using 3 modules from R2API: ItemAPI to add our item, ItemDropAPI to have our item drop ingame, and LanguageAPI to add our language tokens.
    [R2APISubmoduleDependency(nameof(ItemAPI),nameof(ItemDropAPI),nameof(LanguageAPI))]
    

    //This is the main declaration of our plugin class. BepInEx searches for all classes inheriting from BaseUnityPlugin to initialize on startup.
    //BaseUnityPlugin itself inherits from MonoBehaviour, so you can use this as a reference for what you can declare and use in your plugin class: https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    public class AccelItemMod : BaseUnityPlugin
    {
        //Item definations
        private static ItemDef ItemAmp;
        private static ItemDef VoidNet;
        private static ItemDef StrangeCube;
        private static EquipmentDef Duplicator;
        private static ItemDef TwistedAbacus;
        private static ItemDef HoardersMark;

        //The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
            //ItemAmp item definition
            ItemAmp = new ItemDef
            {
                //More on these later
                name = "AcItMo_ITEMAMP_NAME",
                nameToken = "AcItMo_ITEMAMP_NAME", //? Still needed if we are assigning name in the line above?
                pickupToken = "AcItMo_ITEMAMP_PICKUP",
                descriptionToken = "AcItMo_ITEMAMP_DESC",
                loreToken = "AcItMo_ITEMAMP_LORE",
                //The tier determines what rarity the item is: Tier1=white, Tier2=green, Tier3=red, Lunar=Lunar, Boss=yellow, and finally NoTier is generally used for helper items, like the tonic affliction
                tier = ItemTier.Tier1,
                //You can create your own icons and prefabs through assetbundles, but to keep this boilerplate brief, we'll be using question marks.
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = true,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            //Void Net item definition
            VoidNet = new ItemDef
            {
                name = "AcItMo_VOIDNET_NAME",
                nameToken = "AcItMo_VOIDNET_NAME",
                pickupToken = "AcItMo_VOIDNET_PICKUP",
                descriptionToken = "AcItMo_VOIDNET_DESC",
                loreToken = "AcItMo_VOIDNET_LORE",
                tier = ItemTier.Tier2,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                canRemove = true,
                hidden = false
            };

            //Strange Cube item definition
            StrangeCube = new ItemDef
            {
                name = "AcItMo_STRANGECUBE_NAME",
                nameToken = "AcItMo_STRANGECUBE_NAME",
                pickupToken = "AcItMo_STRANGECUBE_PICKUP",
                descriptionToken = "AcItMo_STRANGECUBE_DESC",
                loreToken = "AcItMo_STRANGECUBE_LORE",
                tier = ItemTier.Tier3,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                canRemove = true,
                hidden = false
            };

            
            //Duplicator item definition
            Duplicator = new EquipmentDef
            {
                name = "AcItMo_DUPLICATOR_NAME",
                nameToken = "AcItMo_DUPLICATOR_NAME",
                pickupToken = "AcItMo_DUPLICATOR_PICKUP",
                descriptionToken = "AcItMo_DUPLICATOR_DESC",
                loreToken = "AcItMo_DUPLICATOR_LORE",
                cooldown = 30.0f,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                isLunar = false,
                isBoss = false
            };*/

            //Twisted abacus item definition
            TwistedAbacus = new ItemDef
            {
                name = "AcItMo_TWISTEDABACUS_NAME",
                nameToken = "AcItMo_TWISTEDABACUS_NAME",
                pickupToken = "AcItMo_TWISTEDABACUS_PICKUP",
                descriptionToken = "AcItMo_TWISTEDABACUS_DESC",
                loreToken = "AcItMo_TWISTEDABACUS_LORE",
                tier = ItemTier.Lunar,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                canRemove = true,
                hidden = false
            };

            //Twisted abacus item definition
            HoardersMark = new ItemDef
            {
                name = "AcItMo_HOARDERSMARK_NAME",
                nameToken = "AcItMo_HOARDERSMARK_NAME",
                pickupToken = "AcItMo_HOARDERSMARK_PICKUP",
                descriptionToken = "AcItMo_HOARDERSMARK_DESC",
                loreToken = "AcItMo_HOARDERSMARK_LORE",
                tier = ItemTier.Tier2,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                canRemove = true,
                hidden = false
            };


            //Now let's turn the tokens we made into actual strings for the game:
            AddTokens();

            //You can add your own display rules here, where the first argument passed are the default display rules: the ones used when no specific display rules for a character are found.
            //For this example, we are omitting them, as they are quite a pain to set up.
            var ItemAmpDisplayRules = new ItemDisplayRuleDict(null);
            var VoidNetDisplayRules = new ItemDisplayRuleDict(null);
            var StrangeCubeDisplayRules = new ItemDisplayRuleDict(null);
            var DuplicatorDisplayRules = new ItemDisplayRuleDict(null);
            var TwistedAbacusDisplayRules = new ItemDisplayRuleDict(null);
            var HoardersMarkDisplayRules = new ItemDisplayRuleDict(null);

            //Then finally add it to R2API
            ItemAPI.Add(new CustomItem(ItemAmp, ItemAmpDisplayRules));
            ItemAPI.Add(new CustomItem(VoidNet, VoidNetDisplayRules));
            ItemAPI.Add(new CustomItem(StrangeCube, StrangeCubeDisplayRules));
            ItemAPI.Add(new CustomEquipment(Duplicator, DuplicatorDisplayRules));
            ItemAPI.Add(new CustomItem(TwistedAbacus, TwistedAbacusDisplayRules));
            ItemAPI.Add(new CustomItem(HoardersMark, HoardersMarkDisplayRules));

            //But now we have defined an item, but it doesn't do anything yet. So we'll need to define that ourselves.
            GlobalEventManager.onCharacterDeathGlobal += GlobalEventManager_onCharacterDeathGlobal;

        }

        private void GlobalEventManager_onCharacterDeathGlobal(DamageReport report)
        {
            //If a character was killed by the world, we shouldn't do anything.
            if (!report.attacker || !report.attackerBody )
                return;
            
            CharacterBody attacker = report.attackerBody;
            //We need an inventory to do check for our item
            if (attacker.inventory) {
                //store the amount of our item we have
                int garbCount = attacker.inventory.GetItemCount(myItemDef.itemIndex);
                if (garbCount > 0 &&
                    //Roll for our 5% chance.
                    Util.CheckRoll(5, attacker.master))
                {
                    //Since we passed all checks, we now give our attacker the cloaked buff.
                    attacker.AddTimedBuff(BuffIndex.Cloak, 3 + (garbCount));
                }
            }
        }

        //This function adds the tokens from the item using LanguageAPI, the comments in here are a style guide, but is very opiniated. Make your own judgements!
        private void AddTokens()
        {
            /*
            //The Name should be self explanatory
            R2API.LanguageAPI.Add("EXAMPLE_CLOAKONKILL_NAME", "Cuthroat's Garb");
            //The Pickup is the short text that appears when you first pick this up. This text should be short and to the point, numbers are generally ommited.
            R2API.LanguageAPI.Add("EXAMPLE_CLOAKONKILL_PICKUP", "Chance to cloak on kill");
            //The Description is where you put the actual numbers and give an advanced description.
            R2API.LanguageAPI.Add("EXAMPLE_CLOAKONKILL_DESC", "Whenever you <style=cIsDamage>kill an enemy</style>, you have a <style=cIsUtility>5%</style> chance to cloak for <style=cIsUtility>4s</style> <style=cStack>(+1s per stack)</style.");
            //The Lore is, well, flavor. You can write pretty much whatever you want here.
            R2API.LanguageAPI.Add("EXAMPLE_CLOAKONKILL_LORE", "Those who visit in the night are either praying for a favour, or preying on a neighbour.");
            */

            //item amplifier
            R2API.LanguageAPI.Add("AcItMo_ITEMAMP_NAME", "Item amplifier");
            R2API.LanguageAPI.Add("AcItMo_ITEMAMP_PICKUP", "Amplifies the effect of the highest stacked item");
            R2API.LanguageAPI.Add("AcItMo_ITEMAMP_DESC", "Work in Progress");
            R2API.LanguageAPI.Add("AcItMo_ITEMAMP_LORE", "Work in Progress");

            //Void net
            R2API.LanguageAPI.Add("AcItMo_VOIDNET_NAME", "Void net");
            R2API.LanguageAPI.Add("AcItMo_VOIDNET_PICKUP", "Collect scrap from the void during teleportation");
            R2API.LanguageAPI.Add("AcItMo_VOIDNET_DESC", "Work in Progress");
            R2API.LanguageAPI.Add("AcItMo_VOIDNET_LORE", "Work in Progress");

            //Strange cube
            R2API.LanguageAPI.Add("AcItMo_STRANGECUBE_NAME", "Strange cube");
            R2API.LanguageAPI.Add("AcItMo_STRANGECUBE_PICKUP", "Steals a Stack of items from alternate reality during teleportation");
            R2API.LanguageAPI.Add("AcItMo_STRANGECUBE_DESC", "Work in Progress");
            R2API.LanguageAPI.Add("AcItMo_STRANGECUBE_LORE", "Work in Progress");

            //Duplicator
            R2API.LanguageAPI.Add("AcItMo_DUPLICATOR_NAME", "Duplicator");
            R2API.LanguageAPI.Add("AcItMo_DUPLICATOR_PICKUP", "Duplicates item by using scrap of a similar rarity");
            R2API.LanguageAPI.Add("AcItMo_DUPLICATOR_DESC", "Work in Progress");
            R2API.LanguageAPI.Add("AcItMo_DUPLICATOR_LORE", "Work in Progress");

            //Twisted abacus
            R2API.LanguageAPI.Add("AcItMo_TWISTEDABACUS_NAME", "Twisted abacus");
            R2API.LanguageAPI.Add("AcItMo_TWISTEDABACUS_PICKUP", "There is only white scrap, but scrap prices are doubled");
            R2API.LanguageAPI.Add("AcItMo_TWISTEDABACUS_DESC", "Work in Progress");
            R2API.LanguageAPI.Add("AcItMo_TWISTEDABACUS_LORE", "Work in Progress");

            //Hoader's mark
            R2API.LanguageAPI.Add("AcItMo_HOARDERSMARK_NAME", "Hoarder's mark");
            R2API.LanguageAPI.Add("AcItMo_HOARDERSMARK_PICKUP", "You stronger from hoarding scrap");
            R2API.LanguageAPI.Add("AcItMo_HOARDERSMARK_DESC", "Work in Progress");
            R2API.LanguageAPI.Add("AcItMo_HOARDERSMARK_LORE", "Work in Progress");

        }

        //The Update() method is run on every frame of the game.
        public void Update()
        {
            //This if statement checks if the player has currently pressed F2.
            if (Input.GetKeyDown(KeyCode.F2))
            {
                //Get the player body to use a position:	
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
                //And then drop our defined item in front of the player.
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(ItemAmp.itemIndex), transform.position, transform.forward * 20f);
            }   
        }
    }
}