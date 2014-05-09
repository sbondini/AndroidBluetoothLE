using Java.Util;

namespace AndroidBluetoothLE.Bluetooth
{
    public class BluetoothServiceInfo
    {
        public UUID ServiceUuid { get; private set; }

        public UUID CharacteristicUuid { get; private set; }

        public BluetoothServiceInfo(UUID serviceUuid, UUID characteristicUuid)
        {
            ServiceUuid = serviceUuid;
            CharacteristicUuid = characteristicUuid;
        }

        public BluetoothServiceInfo(string serviceUuid, string characteristicUuid)
            : this(UUID.FromString(serviceUuid), UUID.FromString(characteristicUuid))
        {
        }
    }
}