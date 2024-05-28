using TheKarters2Mods.Patches;

namespace TwitchCommandPack.Commands;

public class ReverseDirectionInputsCommand : ITwitchCommand {
    public static bool isReverseDirectionInputsEnabled = false;
    public static float timeInSeconds = 0f;

    public string CommandFeedback(string _user, string[] _command) {
        return $"Thanks {_user} for breaking the steering wheel...";
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        TwitchCommandPack.Get().logger.Log("cmd executed");

        isReverseDirectionInputsEnabled = !isReverseDirectionInputsEnabled;

        return true;
    }

    public bool IsActivated() {
        TwitchCommandPack.Get().logger.Log("cmd registered");
        return TwitchCommandPack.Get().data.isReverseDirectionInputsCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        if (_command.Length != 3) {
            TwitchCommandPack.Get().logger.Log("not enough parts");
            return false;
        }

        string firstWord = _command[1];
        string secondWord = _command[2];

        if (firstWord != "reverse") {
            TwitchCommandPack.Get().logger.Log("first term is wrong");
            return false;
        }

        if (secondWord != "direction" || secondWord != "directions") {
            TwitchCommandPack.Get().logger.Log("second term is wrong");
            return false;
        }

        return true;
    }
}