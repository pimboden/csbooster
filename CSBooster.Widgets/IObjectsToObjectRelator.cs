using System;

namespace _4screen.CSB.Widget
{
    public interface IObjectsToObjectRelator
    {
        Guid? CommunityId { get; set; }
        Guid? UserId { get; set; }
        Guid? ParentObjectID { get; set; }
		string ChildObjectTypes { get; set; }
		string LabelText { get; set; }
        int MaxChildObjects { get; set; }
        string RelationType { get; set; }
        bool ExcludeSystemObjects{ get; set; }
        bool Save();
    }
}
