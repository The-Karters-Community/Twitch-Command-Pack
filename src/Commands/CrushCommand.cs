using TheKarters2Mods.Patches;
using TheKartersModdingAssistant;

namespace TwitchCommandPack.Commands;

public class CrushCommand: ITwitchCommand {
    public static float timeInSeconds = 5;
    public static Player player;

    public string CommandFeedback(string _user, string[] _command) {
        string feedback = $"{_user} called Thor for crushing {player.GetName()}!";

        return feedback;
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        player = Player.FindMainPlayer();

        HpBarController hpBarController = player.uHpBarController;

        if (hpBarController.hittedByHammerCoroutine is not null) {
            hpBarController.StopCoroutine(hpBarController.hittedByHammerCoroutine);
        }

        hpBarController.kartController.SetHammerGroundFriction_NoY(0.0f);
        hpBarController.hittedByHammerCoroutine = hpBarController.StartCoroutine(hpBarController.HittedByHammer_IncreaseFriction(timeInSeconds));

        player.uAntPlayer.visualInstanceSyncedParams.antVisualKart.kartAnimatorWorker.StartHammerDamage(timeInSeconds);

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isCrushCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        int amountOfTerms = _command.Length - 1;

        if (amountOfTerms > 1) {
            return false;
        }

        string firstTerm = _command[1];

        return firstTerm == "crush";
    }
}