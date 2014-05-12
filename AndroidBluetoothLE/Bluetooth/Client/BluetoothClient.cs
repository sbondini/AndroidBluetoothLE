using System;
using System.Diagnostics;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;

namespace AndroidBluetoothLE.Bluetooth.Client
{
    public class BluetoothClient
    {
        private static BluetoothClient _instance;
        private bool _isInitialized;
        private BluetoothConnectionHandler _connectionHandler; 

        public static BluetoothClient Instance { get { return _instance ?? (_instance = new BluetoothClient()); } }

        public BluetoothManager Manager { get; private set; }

        public BluetoothAdapter Adapter { get; private set; }

        public BluetoothDevice SelectedDevice { get; set; }

        public BluetoothConnectionHandler ConnectionHandler
        {
            get { return _connectionHandler ?? (_connectionHandler = new BluetoothConnectionHandler(Manager)); }
        }

        public bool Initialize()
        {
            if (_isInitialized) return false;

            var context = Application.Context;

            if (!context.PackageManager.HasSystemFeature(PackageManager.FeatureBluetoothLe))
            {
                Debug.WriteLine("Bluetooth LE is not supported");
                return false;
            }
            _isInitialized = true;

            var manager = (BluetoothManager)context.GetSystemService(Context.BluetoothService);
            
            Manager = manager;
            Adapter = manager.Adapter;
            
            return true;
        }

        public void EnableIfNeeded(BluetoothAdapter adapter)
        {
            if (!adapter.IsEnabled)
            {
                adapter.Enable();
            }
        }

        private BluetoothClient()
        {
        }
    }
}