// Code by M.K.
// https://github.com/MKsKartersModTeam/MKsKartersMods/blob/2a4b984915356b7d7f66e02e64318c42f3ccf514/Mods/MirrorMode.cs#L91C83-L91C95

using System.Collections.Generic;
using HarmonyLib;
using TwitchCommandPack.Commands;
using UnityEngine;

namespace TwitchCommandPack.Core;

[HarmonyPatch(typeof(Ant_KartParticlesWorker), nameof(Ant_KartParticlesWorker.WorkerUpdate))]
public static class Ant_KartParticlesWorker__WorkerUpdate {
    private static bool Prefix(Ant_KartParticlesWorker __instance) {
        // Do not execute what's after is the command is not active.
        if (!ScreenMirrorCommand.isEnabled && !ScreenFlipCommand.isEnabled) {
            return true;
        }

        Dictionary<object, Vector3> OrigScales = new();

        // This at least makes the kart particles face the camera
        OrigScales.TryAdd(__instance, __instance.transform.localScale);

        var v = OrigScales[__instance];

        float fx = ScreenMirrorCommand.isEnabled ? -1 : 1;
        float fy = ScreenFlipCommand.isEnabled ? -1 : 1;
        float fz = -1;

        __instance.transform.localScale = new Vector3(v.x * fx, v.y * fy, v.z * fz);

        return true;
    }
}