using System;

public interface IRevMobListener
{
	void RevMobAdDidReceive(string revMobAdType);

	void RevMobAdDidFail(string revMobAdType);

	void RevMobAdDisplayed(string revMobAdType);

	void RevMobUserClickedInTheAd(string revMobAdType);

	void RevMobUserClosedTheAd(string revMobAdType);

	void RevMobInstallDidReceive(string message);

	void RevMobInstallDidFail(string message);
}


