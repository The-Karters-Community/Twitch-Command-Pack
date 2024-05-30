using TheKarters2Mods.Patches;
using TheKartersModdingAssistant;

namespace TwitchCommandPack.Commands;

public class TankCommand: ITwitchCommand {
    public static Player player;

    public string CommandFeedback(string _user, string[] _command) {
        int amountOfTerms = _command.Length - 1;

        if (amountOfTerms == 1) {
            return $"{_user} is wondering if {player.GetName()} has already driven a tank.";
        }

        return $"{_user} wishes to see the AIs conquer the world with tanks!";;
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        int amountOfTerms = _command.Length - 1;

        if (amountOfTerms == 1) {
            player = Player.FindMainPlayer();

            player.uWeaponsController.PickUpWeapon(ItemExt.ToWeaponType(Item.TANK));
            player.uWeaponsController.FireInput(true);

            return true;
        }

        foreach (Player p in Player.GetActivePlayers()) {
            if (p.IsHuman()) {
                continue;
            }

            p.uWeaponsController.PickUpWeapon(ItemExt.ToWeaponType(Item.TANK));
            p.uWeaponsController.FireInput(true);
        }

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isTankCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        int amountOfTerms = _command.Length - 1;

        if (amountOfTerms > 2) {
            return false;
        }

        string firstTerm = _command[1];

        if (firstTerm != "tank") {
            return false;
        }

        if (amountOfTerms == 1) {
            return true;
        }
        
        string secondTerm = _command[2];

        return secondTerm == "ais";
    }
}