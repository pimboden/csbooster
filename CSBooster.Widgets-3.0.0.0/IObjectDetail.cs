using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
    public interface IObjectDetail
    {
        Guid? ObjectID { get; set; }

        Guid? CommunityID { get; set; }

        int ObjType { get; set; }

        bool ShowAuthor { get; set; }

        bool ShowTitle { get; set; }

        bool ShowShortDesc { get; set; }

        bool ShowDesc { get; set; }

        bool ShowImage { get; set; }

        bool ShowCustom1 { get; set; }

        bool ShowCustom2 { get; set; }

        bool ShowCustom3 { get; set; }

        bool ShowCustom4 { get; set; }

        bool ShowCustom5 { get; set; }

        bool ShowRating { get; set; }

        string ImageWidth { get; set; }

        bool ShowItemAuthor { get; set; }

        bool ShowItemTitle { get; set; }

        bool ShowOverviewLink { get; set; }

        bool HasContent { get; set; }

        FolderSort FolderSorting { get; set; }
    }
}