// Code by M.K.
// https://github.com/MKsKartersModTeam/MKsKartersMods/blob/2a4b984915356b7d7f66e02e64318c42f3ccf514/Mods/MirrorMode.cs#L53

using HarmonyLib;
using TwitchCommandPack.Commands;
using UnityEngine;

namespace TwitchCommandPack.Core;

[HarmonyPatch(typeof(PixelSDK_CameraEvents), nameof(PixelSDK_CameraEvents.OnPreCull))]
public static class PixelSDK_CameraEvents__OnPreCull {
    private static void Postfix(PixelSDK_CameraEvents __instance) {
        // Do not execute what's after is the command is not active.
        if (!ReverseScreenCommand.isReverseScreenEnabled) {
            return;
        }

        // Invert the camera projection matrix across the X-axis
        var camera = __instance.parentCamera.unityCamera;
        var mat = camera.projectionMatrix;

        mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        camera.projectionMatrix = mat;
    }
}

[HarmonyPatch(typeof(PixelSDK_CameraEvents), nameof(PixelSDK_CameraEvents.OnPreRender))]
public static class PixelSDK_CameraEvents__OnPreRender {
    private static void Postfix() {
        // Do not execute what's after is the command is not active.
        if (!ReverseScreenCommand.isReverseScreenEnabled) {
            return;
        }

        GL.invertCulling = true;
    }
}

[HarmonyPatch(typeof(PixelSDK_CameraEvents), nameof(PixelSDK_CameraEvents.OnPostRender))]
public static class PixelSDK_CameraEvents__OnPostRender {
    private static void Postfix() {
        // Do not execute what's after is the command is not active.
        if (!ReverseScreenCommand.isReverseScreenEnabled) {
            return;
        }

        GL.invertCulling = false;
    }
}