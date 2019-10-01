namespace Toastapp.MVVM
{
    public class Tuple<T1, T2>
    {
        public T1 GameObject
        {
            get; private set;
        }
        public T2 Type
        {
            get; private set;
        }
        internal Tuple(T1 gameObject, T2 view)
        {
            GameObject = gameObject;
            Type = view;
        }
    }

    public static class Tuple
    {
        public static Tuple<T1, T2> New<T1, T2>(T1 gameObject, T2 viewModel)
        {
            var twin = new Tuple<T1, T2>(gameObject, viewModel);
            return twin;
        }
    }
}