using System.Diagnostics;
using Android.Bluetooth;

namespace AndroidBluetoothLE.Bluetooth.Server
{
    public delegate void CharacteristicWriteRequestEventHandler(BluetoothDevice device, int requestId,
        BluetoothGattCharacteristic characteristic, bool preparedWrite, bool responseNeeded, int offset, byte[] value);

    public class GattServerObserver : BluetoothGattServerCallback
    {
        private static GattServerObserver _instance;

        public static GattServerObserver Instance
        {
            get { return _instance ?? (_instance = new GattServerObserver()); }
        }

        public event CharacteristicWriteRequestEventHandler CharasteristicWriteRequested;

        public override void OnCharacteristicWriteRequest(BluetoothDevice device, int requestId, BluetoothGattCharacteristic characteristic,
            bool preparedWrite, bool responseNeeded, int offset, byte[] value)
        {
            var handler = CharasteristicWriteRequested;
            if (handler != null) handler(device, requestId, characteristic, preparedWrite, responseNeeded, offset, value);
        }

        public override void OnConnectionStateChange(BluetoothDevice device, ProfileState status, ProfileState newState)
        {
            var message = "Bluetooth Server connection state: {0} Device: " + device.Address;
            
            Debug.WriteLineIf(newState == ProfileState.Connected, string.Format(message, "Connected"));
            Debug.WriteLineIf(newState == ProfileState.Disconnected, string.Format(message, "Disconnected"));
        }

        public override void OnServiceAdded(ProfileState status, BluetoothGattService service)
        {
            Debug.WriteLine("Added service " + service.Uuid);
        }
    }
}