using System;
using Android.Bluetooth;

namespace AndroidBluetoothLE.Client
{
    public class BluetoothDeviceScanner : Java.Lang.Object, BluetoothAdapter.ILeScanCallback
    {
        private readonly Action<BluetoothDevice> _onDiscoveredPeripheral;
        private readonly BluetoothAdapter _adapter;

        public BluetoothDeviceScanner(BluetoothAdapter adapter, Action<BluetoothDevice> onDiscoveredPeripheral)
        {
            _adapter = adapter;
            _onDiscoveredPeripheral = onDiscoveredPeripheral;
        }

        public void StartScan()
        {
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