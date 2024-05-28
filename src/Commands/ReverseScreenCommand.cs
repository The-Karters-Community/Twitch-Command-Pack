using TheKarters2Mods.Patches;

namespace TwitchCommandPack.Commands;

public class ReverseScreenCommand : ITwitchCommand {
    public static bool isReverseScreenEnabled = false;
    public static float timeInSeconds = 0f;

    public string CommandFeedback(string _user, string[] _command) {
        if (!isReverseScreenEnabled) {
            return $"Oh {_user} prefers your streamer in their right profile.";
        }

        return $"Yes {_user}, it's time to look your favorite streamer in a mirror.";
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        isReverseScreenEnabled = !isReverseScreenEnabled;

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isReverseScreenCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        if (_command.Length != 3) {
            return false;
        }

        string firstWord = _command[1];
        string secondWord = _command[2];

        if (firstWord != "reverse") {
            return false;
        }

        return secondWord == "screen";
    }
}