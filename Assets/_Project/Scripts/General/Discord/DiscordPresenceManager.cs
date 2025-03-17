using Discord;
using System;
using UnityEngine;
using diagnostic = System.Diagnostics;

public class DiscordPresenceManager : MonoBehaviour {
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private MoneyManager _moneyManager;

    private Discord.Discord _discord;
    private string _activityState;
    private string _activityDetails;
    private const long CLIENT_ID = 1350139391686873201;

    private void Awake() {
        CheckDiscordInstalled();
    }

    private void CheckDiscordInstalled() {
        bool haveDiscord = false;
        diagnostic.Process[] processes = diagnostic.Process.GetProcesses();

        for (int i = 0; i < processes.Length; i++) {
            if (processes[i].ToString() == "System.Diagnostics.Process (Discord)") {
                haveDiscord = true;
                break;
            }
        }

        if (!haveDiscord)
            return;

        CreateRichPresence();
    }

    private void CreateRichPresence() {
        _discord = new Discord.Discord(CLIENT_ID, (UInt64)CreateFlags.Default);

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
        if (_discord == null)
            return;

        _discord.RunCallbacks();
    }

    private void OnApplicationQuit() {
        if (_discord == null)
            return;

        ActivityManager activityManager = _discord.GetActivityManager();
        activityManager.ClearActivity((res) => { });
    }
}
