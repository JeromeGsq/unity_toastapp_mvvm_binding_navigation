namespace UnityWeld.Binding.Adapters
{
    using System;

    [Adapter(typeof(Single), typeof(string))]
    public class SingleToPercentAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            var single = (Single)valueIn;
            return single.ToString() + "/100";
        }
    }
}