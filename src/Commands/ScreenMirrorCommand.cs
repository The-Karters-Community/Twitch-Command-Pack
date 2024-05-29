using TheKarters2Mods.Patches;

namespace TwitchCommandPack.Commands;

public class ScreenMirrorCommand : ITwitchCommand {
    public static bool isEnabled = false;

    public string CommandFeedback(string _user, string[] _command) {
        if (!isEnabled) {
            return $"Oh {_user} prefers your streamer in their right profile.";
        }

        return $"Yes {_user}, it's time to look your favorite streamer in a mirror.";
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        isEnabled = !isEnabled;

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isScreenMirrorCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        if (_command.Length != 3) {
            return false;
        }

        string firstWord = _command[1];
        string secondWord = _command[2];

        if (firstWord != "screen" || secondWord != "mirror") {
            return false;
        }

        return true;
    }
}