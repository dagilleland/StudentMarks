using System;

namespace StudentMarks.Framework.CourseEvaluation.Domain
{
    public abstract class CourseNumberAlreadyExists : Exception { }
    public abstract class CourseNameAlreadyExists : Exception { }
    public abstract class CourseNotFound : Exception { }
    public abstract class PassMarkIsInvalid : Exception { }
    public abstract class InvalidEvaluationComponentWeight : Exception { }
    public abstract class IncorrectTotalEvaluationComponentWeight : Exception { }
    public abstract class CourseNotAvailable : Exception { }
    public abstract class CoursePreviouslyReleased : Exception { }
}
