using System;

namespace StudentMarks.Framework.CourseEvaluation.Domain
{
    public class CourseDuplication : Exception { }
    public class CourseNumberInvalid : Exception { }
    public class CourseNameInvalid : Exception { }
    public class IdentityMismatch : Exception { }
    public class CourseNotFound : Exception { }
    public class PassMarkIsInvalid : Exception { }
    public class PassMarkCannotBeChanged : Exception { }
    public class InvalidEvaluationComponentWeight : Exception { }
    public class IncorrectTotalEvaluationComponentWeight : Exception { }
    public class CourseNotAvailable : Exception { }
    public class CoursePreviouslyReleased : Exception { }
}
