using Android.Bluetooth;
using Java.Util;

namespace AndroidBluetoothLE.Bluetooth.Server
{
    public class BluetoothServiceFactory
    {
        public BluetoothGattService CreateService()
        {
            var serviceUuid = UUID.FromString(DeviceUuidContainer.FeedbackService);
            var service = new BluetoothGattService(serviceUuid, GattServiceType.Primary);

            service.AddCharacteristic(new BluetoothGattCharacteristic(UUID.FromString(DeviceUuidContainer.HostnameCharacteristic),
                GattProperty.WriteNoResponse, GattPermission.Write));

            service.AddCharacteristic(new BluetoothGattCharacteristic(UUID.FromString(DeviceUuidContainer.PayloadCharacteristic),
                GattProperty.WriteNoResponse, GattPermission.Write));

            service.AddCharacteristic(new BluetoothGattCharacteristic(UUID.FromString(DeviceUuidContainer.PortCharacteristic),
                GattProperty.Write, GattPermission.Write));

            return service;
        }
    }
}