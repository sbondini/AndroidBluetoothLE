using System;
using System.Diagnostics;
using Android.App;
using Android.Bluetooth;
using Android.Content;

namespace AndroidBluetoothLE.Bluetooth.Client
{
    public class DevicePairingHandler : BroadcastReceiver
    {
        private readonly BluetoothGatt _gatt;
        private readonly GattClientObserver _gattObserver;
        private Action<bool> _onPairingFinished;

        public DevicePairingHandler(BluetoothGatt gatt, GattClientObserver gattObserver)
        {
            _gatt = gatt;
            _gattObserver = gattObserver;
            Application.Context.RegisterReceiver(this, new IntentFilter(BluetoothDevice.ActionBondStateChanged));
        }

        public void Pair(Action<bool> onPairingFinished)
        {
            _onPairingFinished = onPairingFinished;
            var observingHandler = new DeviceObservingHandler(_gatt, _gattObserver);

            var serviceInfo = new BluetoothServiceInfo(DeviceUuidContainer.UartService,
                DeviceUuidContainer.NotifyCharacteristic);
            observingHandler.Subscribe(serviceInfo, b => observingHandler.Unsubscribe(serviceInfo));
        }


        public override void OnReceive(Context context, Intent intent)
        {
            var device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
            Debug.WriteLine("Result of trying to pair with pager: " + device.BondState);

            if (device.BondState != Bond.Bonding && _onPairingFinished != null)
            {
                _onPairingFinished(device.BondState == Bond.Bonded);
            }
        }
    }
}