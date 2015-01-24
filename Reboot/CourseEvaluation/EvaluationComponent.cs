using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentMarks.Framework.CourseEvaluation
{
    public class EvaluationComponent
    {
        public string Name { get; private set; }
        public int Weight { get; private set; }
        public EvaluationComponent(string name, int weight)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("A name is required for an evaluation component");
            if (weight <= 0 || weight > 100)
                throw new ArgumentException("A weight between 1 and 100 is required for an evaluation component");

            Name = name;
            Weight = weight;
        }
    }
}
