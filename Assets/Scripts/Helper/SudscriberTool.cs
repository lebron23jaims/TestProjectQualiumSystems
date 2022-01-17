using System.Collections.Generic;

namespace Helper
{
    public interface ISubscriber
    {
        void Subscribe();
        void UnSubscribe();
    }

    public static class SudscriberTool
    {
        public static List<ISubscriber> Subscribers = new List<ISubscriber>();

        public static void AddSubscriber(ISubscriber subscriber)
        {
            if (!Subscribers.Contains(subscriber))
            {
                Subscribers.Add(subscriber);
                subscriber.Subscribe();
            }
        }

        public static void UnsubscribeAll()
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.UnSubscribe();
            }
        }

        public static void Reset()
        {
            Subscribers.Clear();
        }
    }
}

