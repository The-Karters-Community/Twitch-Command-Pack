// Code by M.K.
// https://github.com/MKsKartersModTeam/MKsKartersMods/blob/2a4b984915356b7d7f66e02e64318c42f3ccf514/Mods/MirrorMode.cs#L91C83-L91C95

using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace TwitchCommandPack.Core;

[HarmonyPatch(typeof(Ant_KartParticlesWorker), nameof(Ant_KartParticlesWorker.WorkerUpdate))]
public static class Ant_KartParticlesWorker__WorkerUpdate {
    private static bool Postfix(Ant_KartParticlesWorker __instance) {
        Dictionary<object, Vector3> OrigScales = new();

        // This at least makes the kart particles face the camera
        OrigScales.TryAdd(__instance, __instance.transform.localScale);

        var v = OrigScales[__instance];

        __instance.transform.localScale = new Vector3(v.x * -1, v.y, v.z * -1);

        return true;
    }
}