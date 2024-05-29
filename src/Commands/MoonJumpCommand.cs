using TheKarters2Mods.Patches;
using TheKartersModdingAssistant;
using UnityEngine;

namespace TwitchCommandPack.Commands;

public class MoonJumpCommand: ITwitchCommand {
    public Player player;

    public int minJumpStrength = 100;
    public int maxJumpStrength = 200;

    public string CommandFeedback(string _user, string[] _command) {
        string feedback = $"{_user} loves to watch {player.GetName()} flying below this beautiful sun!";

        return feedback;
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        int amountOfTerms = _command.Length - 1;
        int jumpStrength;

        if (amountOfTerms == 1) {
            jumpStrength = maxJumpStrength;
        } else {
            jumpStrength = Mathf.Clamp(int.Parse(_command[2]), minJumpStrength, maxJumpStrength);
        }

        player = Player.FindMainPlayer();

        player.uPixelKartPhysics.kartController.AddVelocity(Vector3.up * jumpStrength);

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isMoonJumpCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        int amountOfTerms = _command.Length - 1;

        if (amountOfTerms < 1 || amountOfTerms > 2) {
            return false;
        }

        string firstTerm = _command[1];

        if (firstTerm != "moonjump") {
            return false;
        }

        if (amountOfTerms == 2) {
            string secondTerm = _command[2];

            if (!int.TryParse(secondTerm, out _)) {
                return false;
            }
        }

        return true;
    }
}