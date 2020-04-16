using System;
using System.Collections.Generic;
using System.Text;

namespace Corezine.Services.Entities
{
    public class FeedbackEntity
    {
        public List<String> MessageRecords { get; set; } = new List<String>();
        public List<String> InfoRecords { get; set; } = new List<String>();
        public List<String> SuccessRecords { get; set; } = new List<String>();
        public List<String> WarningRecords { get; set; } = new List<String>();
        public List<String> ErrorRecords { get; set; } = new List<String>();
        public List<String> DebugRecords { get; set; } = new List<String>();
    }
}
