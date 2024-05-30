using TheKarters2Mods.Patches;
using TheKartersModdingAssistant;

namespace TwitchCommandPack.Commands;

public class TankCommand: ITwitchCommand {
    public static Player player;

    public string CommandFeedback(string _user, string[] _command) {
        string feedback = $"{_user} is wondering if {player.GetName()} has already driven a tank.";

        return feedback;
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        player = Player.FindMainPlayer();

        player.uWeaponsController.PickUpWeapon(ItemExt.ToWeaponType(Item.TANK));
        player.uWeaponsController.FireInput(true);

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isTankCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        int amountOfTerms = _command.Length - 1;

        if (amountOfTerms > 1) {
            return false;
        }

        string firstTerm = _command[1];

        return firstTerm == "tank";
    }
}