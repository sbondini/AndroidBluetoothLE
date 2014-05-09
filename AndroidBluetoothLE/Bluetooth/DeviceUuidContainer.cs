namespace AndroidBluetoothLE.Bluetooth
{
    public static class DeviceUuidContainer
    {
        private const string BaseUuid = "F2D3{0}{1}-C437-11E2-883D-0002A5D5C51B";
        private const string FeedbackId = "03";
        private const string MessagingId = "02";
        private const string UartId = "01";

        public static string FeedbackService { get { return string.Format(BaseUuid, FeedbackId, "00"); } }
        public static string PortCharacteristic { get { return string.Format(BaseUuid, FeedbackId, "01"); } }
        public static string HostnameCharacteristic { get { return string.Format(BaseUuid, FeedbackId, "02"); } }
        public static string PayloadCharacteristic { get { return string.Format(BaseUuid, FeedbackId, "03"); } }

        public static string MessagingService { get { return string.Format(BaseUuid, MessagingId, "00"); } }
        public static string AddressCharacteristic { get { return string.Format(BaseUuid, MessagingId, "01"); } }
        public static string TextCharacteristic { get { return string.Format(BaseUuid, MessagingId, "02"); } }

        public static string UartService { get { return string.Format(BaseUuid, UartId, "00"); } }
        public static string WriteCharacteristic { get { return string.Format(BaseUuid, UartId, "01"); } }
        public static string NotifyCharacteristic { get { return string.Format(BaseUuid, UartId, "02"); } }
    }
}