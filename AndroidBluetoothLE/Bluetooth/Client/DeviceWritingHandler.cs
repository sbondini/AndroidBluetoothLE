using System.Collections.Generic;
using System.Linq;
using Android.Bluetooth;
using AndroidBluetoothLE.Extension;
using Java.Util;

namespace AndroidBluetoothLE.Bluetooth.Client
{
    public delegate void ReceivedWriteResponceEventHandler(
        BluetoothGattCharacteristic characteristic, GattStatus status);

    public class DeviceWritingHandler
    {
        private readonly BluetoothGatt _gatt;
        private readonly List<UUID> _needResponceList;

        public event ReceivedWriteResponceEventHandler ReceivedWriteResponce;

        public DeviceWritingHandler(BluetoothGatt gatt, GattClientObserver gattObserver)
        {
            _gatt = gatt;
            _needResponceList = new List<UUID>();
            gattObserver.CharacteristicWritten += OnCharacteristicWritten;
        }

        private void OnCharacteristicWritten(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status)
        {
            var responceUuid = _needResponceList.FirstOrDefault(uuid => uuid.IsEqual(characteristic.Uuid));

            if (responceUuid != null)
            {
                var handler = ReceivedWriteResponce;
                if (handler != null) handler(characteristic, status);
            }
        }

        public void Write(IEnumerable<byte[]> buffer, BluetoothServiceInfo serviceInfo, bool withResponce = false)
        {
            var service = _gatt.GetService(serviceInfo.ServiceUuid);
            var characteristic = service.GetCharacteristic(serviceInfo.CharacteristicUuid);

            WriteValueInternal(buffer, characteristic, withResponce);
        }

        private void WriteValueInternal(IEnumerable<byte[]> buffer, BluetoothGattCharacteristic characteristic, bool withResponce)
        {
            foreach (var data in buffer)
            {
                characteristic.SetValue(data);
                characteristic.WriteType = withResponce ? GattWriteType.Default : GattWriteType.NoResponse;
                _gatt.WriteCharacteristic(characteristic);
            }
        }
    }
}