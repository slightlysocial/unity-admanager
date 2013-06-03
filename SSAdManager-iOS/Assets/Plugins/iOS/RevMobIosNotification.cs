using UnityEngine;
using System;
using System.Runtime.InteropServices;

#if UNITY_IPHONE
public class RevMobIosNotification : RevMobNotification {

    public RevMobIosNotification(string placementId) {
    }

    public override void Schedule() {
    }

    public override void Cancel() {
    }
}
#endif