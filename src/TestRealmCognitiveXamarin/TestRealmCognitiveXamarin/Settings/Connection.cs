namespace TestRealmCognitiveXamarin.Settings
{
    public static class Connection
    {
        const string IP = "IP.REALM.OBJECT.SERVER:9080";
        public static readonly string RealmServerUrl = $"realm://{IP}/~/shoppinglist";
        public static readonly string AuthUrl = $"http://{IP}";
    }
}
