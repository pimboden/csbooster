// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;

namespace _4screen.CSB.DataAccess.Business
{
    public static class MainTags
    {
        public static List<MainTag> Load(int? parentId, int? level)
        {
            return Data.MainTags.Load(parentId, level);
        }
    }

    public class MainTag
    {
        internal MainTag()
        {
        }

        public int Id { get; internal set; }
        public int? ParentId { get; internal set; }
        public int Level { get; internal set; }
        public int Order { get; internal set; }
        public string Title { get; internal set; }
        public Guid TagWordId { get; internal set; }
    }
}