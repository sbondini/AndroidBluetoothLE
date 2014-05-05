using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Bluetooth;
using Android.Views;
using Android.OS;
using Android.Widget;
using AndroidBluetoothLE.Client;

namespace AndroidBluetoothLE
{
    [Activity(Label = "BluetoothLE", MainLauncher = true)]
    public class HomeScanView : Activity
    {
        private readonly List<BluetoothDevice> _deviceList;
        private ArrayAdapter<string> _adapter;

        public HomeScanView()
        {
            _deviceList = new List<BluetoothDevice>();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ScanView);
            CreateListView();
        }

        protected override void OnStart()
        {
            base.OnStart();
            StartScanning();
        }

        public void StartScanning()
        {
            var bluetoothClient = new BluetoothClient();
            if (bluetoothClient.Initialize())
            {
                var scanner = new BluetoothDeviceScanner(bluetoothClient.Adapter, OnDiscoveredPeripheral);
                scanner.StartScan();
            }
        }

        private void CreateListView()
        {
            var listView = FindViewById<ListView>(Resource.Id.ScanList);
            var adapter = new ArrayAdapter<string>(this, Resource.Layout.ScanRow, Resource.Id.label);
            
            listView.Adapter = adapter;
            _adapter = adapter;
        }

        private void OnDiscoveredPeripheral(BluetoothDevice device)
        {
            if (_deviceList.All(d => !d.Address.Equals(device.Address, StringComparison.OrdinalIgnoreCase)))
            {
                _deviceList.Add(device);
                RunOnUiThread(() =>
                {
                    _adapter.Add(device.Name);
                    _adapter.NotifyDataSetChanged();
                });
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.ScanMenu, menu);

            return base.OnCreateOptionsMenu(menu);
        }
    }
}

