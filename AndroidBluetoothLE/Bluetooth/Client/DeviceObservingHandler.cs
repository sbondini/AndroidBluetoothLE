using System;
using System.Linq;
using Android.Bluetooth;
using Java.Util;

namespace AndroidBluetoothLE.Bluetooth.Client
{
    public class DeviceObservingHandler
    {
        private readonly BluetoothGatt _gatt;
        private Action<bool> _onSubscribed;

        public DeviceObservingHandler(BluetoothGatt gatt, GattClientObserver gattObserver)
        {
            _gatt = gatt;
            gattObserver.CharacteristicValueChanged += OnCharacteristicValueChanged;
            gattObserver.DescriptorWritten += OnDescriptorWritten;
        }

        private void OnDescriptorWritten(BluetoothGatt gatt, BluetoothGattDescriptor descriptor, GattStatus status)
        {
            if (status == GattStatus.Success && _onSubscribed != null)
            {
                _onSubscribed(true);
            }
        }

        public void Subscribe(BluetoothServiceInfo serviceInfo, Action<bool> onSubscribed)
        {
            _onSubscribed = onSubscribed;

            var characteristic = _gatt.GetService(serviceInfo.ServiceUuid)
                                      .GetCharacteristic(serviceInfo.CharacteristicUuid);

            SubscribeCharacteristic(characteristic);
        }

        public void Unsubscribe(BluetoothServiceInfo serviceInfo)
        {
            var characteristic = _gatt.GetService(serviceInfo.ServiceUuid)
                                      .GetCharacteristic(serviceInfo.CharacteristicUuid);
            _gatt.SetCharacteristicNotification(characteristic, false);
        }

        private void OnCharacteristicValueChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic)
        {
            
        }

        private void SubscribeCharacteristic(BluetoothGattCharacteristic characteristic)
        {
            _gatt.SetCharacteristicNotification(characteristic, true);

            var descriptor = characteristic.GetDescriptor(UUID.FromString("00002902-0000-1000-8000-00805f9b34fb"));
            descriptor.SetValue(BluetoothGattDescriptor.EnableNotificationValue.ToArray());
            _gatt.WriteDescriptor(descriptor);
        }
    }
}