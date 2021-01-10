using System;

namespace KPU_Comment_cleaner.Model
{
    public abstract class Component
    {
        public string Title { get; protected set; }
        public string Content { get; protected set; }
        public string URL { get; protected set; }
        public DateTime DateTime { get; protected set; }
    }
}
