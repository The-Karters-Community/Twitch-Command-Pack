using TheKarters2Mods.Patches;

namespace TwitchCommandPack.Commands;

public class ReverseDirectionInputsCommand : ITwitchCommand {
    public static bool isReverseDirectionInputsEnabled = false;
    public static float timeInSeconds = 0f;

    public string CommandFeedback(string _user, string[] _command) {
        return $"Thanks {_user} for breaking the steering wheel...";
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        isReverseDirectionInputsEnabled = !isReverseDirectionInputsEnabled;

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isReverseDirectionInputsCommandEnabled;
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

        if (secondWord != "direction" || secondWord != "directions") {
            return false;
        }

        return true;
    }
}