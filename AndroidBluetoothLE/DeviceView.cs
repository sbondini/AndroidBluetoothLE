using System;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.OS;
using Android.Widget;
using AndroidBluetoothLE.Bluetooth.Client;
using AndroidBluetoothLE.Bluetooth.Server;

namespace AndroidBluetoothLE
{
    [Activity(Label = "DeviceView")]
    public class DeviceView : Activity
    {
        private Dialog _currentDialog;
        private BluetoothConnectionHandler _connectionHandler;
        private TextView _textView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DeviceView);
            _connectionHandler = new BluetoothConnectionHandler(BluetoothClient.Instance.Manager);
            BluetoothServer.Instance.ReceivedMessage += ServerOnReceivedMessage;

            FindViewById<Button>(Resource.Id.PairingButton).Click += PairingButtonOnClick;
            FindViewById<Button>(Resource.Id.SendDataButton).Click += SendDataOnClick;
            _textView = FindViewById<TextView>(Resource.Id.TextView);
        }

        private async void OnReconnect(object sender, EventArgs eventArgs)
        {
//            _connectionHandler.Disconnect();
//            ShowDialog("Disconnecting...");
//            await Task.Delay(2000);
//            ConnectDevice();
        }

        private void ServerOnReceivedMessage(string text)
        {
            _textView.Text = string.Format("{0}\n{1}", _textView.Text, text);
        }

        private void SendDataOnClick(object sender, EventArgs eventArgs)
        {
            var t = _connectionHandler.IsConnected;
        }

        private void PairingButtonOnClick(object sender, EventArgs eventArgs)
        {
            var pairingHandler = new DevicePairingHandler(_connectionHandler.GattValue, GattClientObserver.Instance);
            pairingHandler.Pair(result =>
            {
                _textView.Text = result ? "Paired Sussessfully" : "Failed to Pair";
            });
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
            CloseDialog();
            if (profileState == ProfileState.Disconnected)
            {
                FinishActivity(123);
            }
        }

        private void ShowDialog(string message)
        {
            CloseDialog();

            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            
            _currentDialog = builder.Create();
            _currentDialog.Show();
        }

        private void CloseDialog()
        {
            if (_currentDialog != null && _currentDialog.IsShowing)
            {
                _currentDialog.Dismiss();
            }
        }
    }
}