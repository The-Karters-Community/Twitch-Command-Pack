using TheKarters2Mods.Patches;

namespace TwitchCommandPack.Commands;

public class ReverseDirectionInputsCommand : ITwitchCommand {
    public static bool isEnabled = false;

    public string CommandFeedback(string _user, string[] _command) {
        if (!isEnabled) {
            return $"Everyone thanks {_user} for repairing the steering wheel!";
        }

        return $"{_user}, what did you do to the steering wheel?!";
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        isEnabled = !isEnabled;

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

        return secondWord == "direction" || secondWord == "directions";
    }
}