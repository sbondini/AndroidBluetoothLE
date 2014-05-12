using System;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidBluetoothLE.Bluetooth.Client;

namespace AndroidBluetoothLE
{
    [Activity(Label = "DeviceView")]
    public class DeviceView : Activity
    {
        private Dialog _currentDialog;
        private BluetoothConnectionHandler _connectionHandler;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DeviceView);
            _connectionHandler = BluetoothClient.Instance.ConnectionHandler;

            FindViewById<Button>(Resource.Id.DiscoverServicesButton).Click += DiscoverServicesButtonClick;
        }

        private async void OnReconnect(object sender, EventArgs eventArgs)
        {
//            _connectionHandler.Disconnect();
//            ShowDialog("Disconnecting...");
//            await Task.Delay(2000);
//            ConnectDevice();
        }

        private void DiscoverServicesButtonClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(ServiceListView));
            StartActivity(intent);
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (!_connectionHandler.IsConnected)
            {
                ConnectDevice();
            }
        }

        private void ConnectDevice()
        {
            var client = BluetoothClient.Instance;
            ShowDialog("Connecting Device...");
            _connectionHandler.Connect(client.SelectedDevice, OnConnectionChanged);
        }

        private void OnConnectionChanged(ProfileState profileState)
        {
            DialogView.CloseDialog(this);
            if (profileState == ProfileState.Disconnected)
            {
                FinishActivity(123);
            }
        }
        
        private void ShowDialog(string message)
        {
            DialogView.ShowDialog(message, this);
        }
    }
}