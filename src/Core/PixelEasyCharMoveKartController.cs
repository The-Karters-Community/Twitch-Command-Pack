// Code by M.K.
// https://github.com/MKsKartersModTeam/MKsKartersMods/blob/2a4b984915356b7d7f66e02e64318c42f3ccf514/Mods/MirrorMode.cs#L78C5-L89C6

using HarmonyLib;
using TheKartersModdingAssistant;
using TwitchCommandPack.Commands;

namespace TwitchCommandPack.Core;

[HarmonyPatch(typeof(PixelEasyCharMoveKartController), nameof(PixelEasyCharMoveKartController.SteerInput))]
public static class PixelEasyCharMoveKartController__SteerInput {
    private static bool Prefix(PixelEasyCharMoveKartController __instance, ref float __0) {
        bool onlyIsReverseDirectionInputsEnabled = ReverseDirectionInputsCommand.isReverseDirectionInputsEnabled && !ReverseScreenCommand.isReverseScreenEnabled;
        bool onlyIsReverseScreenEnabled = !ReverseDirectionInputsCommand.isReverseDirectionInputsEnabled && ReverseScreenCommand.isReverseScreenEnabled;

        // Do not execute what's after is the command is not active.
        if (!onlyIsReverseDirectionInputsEnabled && !onlyIsReverseScreenEnabled) {
            return true;
        }

        Player player = Player.FindByAntPlayer(__instance.parentPlayer);

        // Only invert the character's turning input if this is a human player
        if (player.IsHuman()) {
            __0 *= -1;
        }

        return true;
    }
}