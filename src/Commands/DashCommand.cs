using TheKarters2Mods.Patches;
using TheKartersModdingAssistant;

namespace TwitchCommandPack.Commands;

public class DashCommand: ITwitchCommand {
    public static Player player;

    public string CommandFeedback(string _user, string[] _command) {
        string feedback = $"{_user} has a small surprise!";

        return feedback;
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        player = Player.FindMainPlayer();

        player.uWeaponsController.PickUpWeapon(ItemExt.ToWeaponType(Item.TELEPORTER));
        player.uWeaponsController.FireInput(true);

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isDashCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        int amountOfTerms = _command.Length - 1;

        if (amountOfTerms > 1) {
            return false;
        }

        string firstTerm = _command[1];

        return firstTerm == "dash";
    }
}