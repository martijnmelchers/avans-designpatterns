namespace Core.Extensions
{
    public class Singleton<T> where T : new()
    {
        private static T _instance; 
        public static T GetInstance => _instance ??= new T();
    }
}