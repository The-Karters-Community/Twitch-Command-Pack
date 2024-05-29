using TheKarters2Mods.Patches;

namespace TwitchCommandPack.Commands;

public class ScreenFlipCommand : ITwitchCommand {
    public static bool isEnabled = false;

    public string CommandFeedback(string _user, string[] _command) {
        if (!isEnabled) {
            return $"{_user} is our doctor!";
        }

        return $"{_user} makes everyone dizzy...";
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        isEnabled = !isEnabled;

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isScreenFlipCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        if (_command.Length != 3) {
            return false;
        }

        string firstWord = _command[1];
        string secondWord = _command[2];

        if (firstWord != "screen" || secondWord != "flip") {
            return false;
        }

        return true;
    }
}