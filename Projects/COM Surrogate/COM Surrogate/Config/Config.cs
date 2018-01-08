using System;
using System.Collections.Generic;

namespace Config
{
    [Serializable]
    public class Config
    {
        public List<Message> Messages { get; set; }
    }
}