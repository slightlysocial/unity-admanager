using UnityEngine;
using System;
using System.Runtime.InteropServices;

#if UNITY_ANDROID
public class RevMobAndroidNotification : RevMobNotification {
    private AndroidJavaObject javaObject;

    public RevMobAndroidNotification(AndroidJavaObject javaObject) {
        this.javaObject = javaObject;
    }

    public override void Schedule() {
        javaObject.Call<bool>("schedule");
    }

    public override void Cancel() {
        javaObject.Call<bool>("cancel");
    }
}
#endif