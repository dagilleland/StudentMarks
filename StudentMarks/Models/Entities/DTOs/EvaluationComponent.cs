using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMarks.Models.Entities.DTOs
{
    [Obsolete("")]
    public class Course_Specific_EvaluationComponent
    {
        public List<Component> MarkableItems { get; set; }
        public List<Topic> BucketTopics { get; set; }
        public int BucketWeights { get; set; }
    }
    public class Component
    {
        public int? ID { get; set; }
        public string ItemType { get; set; }
        public string Name { get; set; }
        public int? Weight { get; set; }
        public int? TotalPossibleMarks { get; set; }
        public string Topic { get; set; }
        public int? TopicID { get; set; }
        public int DisplayOrder { get; set; }

        public Component() { }

        public Component(Bucket info)
        {
            ID = info.Id;
            ItemType = "Bucket";
            Name = info.Name;
            Weight = info.Weight;
            Topic = info.Topic.Description;
            TopicID = info.TopicID;
            DisplayOrder = info.DisplayOrder;
        }

        public Component(Quiz info)
        {
            ID = info.Id;
            ItemType = "Quiz";
            Name = info.Name;
            Weight = info.Weight;
            TotalPossibleMarks = info.PotentialMarks;
            DisplayOrder = info.DisplayOrder;
        }
    }
}