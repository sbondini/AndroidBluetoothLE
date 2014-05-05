using System;
using System.Diagnostics;
using Android.Bluetooth;

namespace AndroidBluetoothLE.Client
{
    public class BluetoothDeviceScanner : Java.Lang.Object, BluetoothAdapter.ILeScanCallback
    {
        private Action<BluetoothDevice> _onDiscoveredPeripheral;
        private readonly BluetoothAdapter _adapter;

        public BluetoothDeviceScanner(BluetoothAdapter adapter)
        {
            _adapter = adapter;
        }

        public void StartScan(Action<BluetoothDevice> onDiscoveredPeripheral)
        {
            _onDiscoveredPeripheral = onDiscoveredPeripheral;
            _adapter.StartLeScan(this);
        }

        public void StopScan()
        {
            _adapter.StopLeScan(this);
        }

        public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
        {
            if (_onDiscoveredPeripheral == null) return;
            _onDiscoveredPeripheral(device);
        }
    }
}