namespace UnityWeld.Binding.Adapters
{
    [Adapter(typeof(object), typeof(string))]
    public class ObjectToStringAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            return valueIn?.ToString() ?? "-- error --";
        }
    }
}


