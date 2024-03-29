using System.Collections.Generic;

namespace RestSharpAutomation.DropBoxAPI.ListFolderModel
{
    public class RootObject
    {
        public List<Entry> entries { get; set; }
        public string cursor { get; set; }
        public bool has_more { get; set; }
    }
}
