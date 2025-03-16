using Discord;
using System;
using UnityEngine;

public class DiscordPresenceManager : MonoBehaviour {
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private MoneyManager _moneyManager;

    private DiscordSDK _discord;
    private string _activityState;
    private string _activityDetails;
    private const long CLIENT_ID = 1350139391686873201;

    private void Awake() {
        _discord = new DiscordSDK(CLIENT_ID, (UInt64)CreateFlags.Default);

        if (ScenesManager.IsGame()) {
            _locationManager.LocationChanged += UpdateActivityState;
            _moneyManager.MoneyChanged += UpdateActivityDetails;
        } else {
            _activityState = "In menu";
            _activityDetails = string.Empty;
            UpdateActivity();
        }
    }

    private void UpdateActivityState(Location location) {
        _activityState = "In " + location.ToString().ToLower();
        UpdateActivity();
    }

    private void UpdateActivityDetails(int money) {
        _activityDetails = "$" + CountConverter.ToCount(money);
        UpdateActivity();
    }

    private void UpdateActivity() {
        ActivityManager activityManager = _discord.GetActivityManager();
        Activity activity = new() {
            State = _activityState,
            Details = _activityDetails
        };
        activityManager.UpdateActivity(activity, (res) => { });
    }

    private void Update() {
        _discord.RunCallbacks();
    }
}
