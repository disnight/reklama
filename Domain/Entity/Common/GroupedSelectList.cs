using System.Collections.Generic;

namespace Domain.Entity.Common
{
    public class GroupedSelectList
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsCurrent { get; set; }

        public List<GroupedSelectList> Children { get; set; }
    }
}