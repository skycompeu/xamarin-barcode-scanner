using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.Generic
{

    public static class GlobalInstance<T> where T : new()
    {
        private static T m_Instance;
        private static readonly object m_Lock = new object();
        private static string m_InstanceId;

        static GlobalInstance()
        {

        }
        
        public static T GetInstance()
        {
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = new T();
                    m_InstanceId = Guid.NewGuid().ToString();
                }

                return m_Instance;
            }
        }

        public static string GetInstanceId()
        {
            return m_InstanceId;
        }
    }
}
