using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMarks.Models.Entities.DTOs
{
    public class EvaluationComponent
    {
        public List<MarkableItem> MarkableItems { get; set; }
        public List<Topic> BucketTopics { get; set; }
        public int BucketWeights { get; set; }
    }
}