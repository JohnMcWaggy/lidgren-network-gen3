using System.Collections.Generic;

namespace Lidgren.Network
{
    public static class ConnectionListPool
    {
        private static Stack<List<NetConnection>> _pool;

        static ConnectionListPool()
        {
            _pool = new Stack<List<NetConnection>>();
        }

        public static List<NetConnection> Rent()
        {
            lock (_pool)
            {
                if (_pool.Count > 0)
                    return _pool.Pop();
            }
            return new List<NetConnection>();
        }

        public static void Return(List<NetConnection> list)
        {
            lock (_pool)
            {
                if (_pool.Count < 32)
                {
                    list.Clear();
                    _pool.Push(list);
                }
            }
        }
    }
}