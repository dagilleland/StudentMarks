# Course Evaluation Bounded Context

## The Domain

For this bounded context, we'll work in the **Course Evaluation** domain. Our focus will be on the concept of a **Course**, which represents some designated area of study. To begin with, a *course* has to be **assigned** a name and a number to distinguish it from other courses. For the course, a *pass mark* is **fixed** as the minimum requirements to gain credit for the course. *Evaluation Components* are **set** for the course to determine the mark distribution in demonstrating competancy in the course. Once the evaluation components are complete, then the course can be **made available** for use in the Course Offering bounded context. A course that is available for offerings can be **reevaluated** (thus, no longer available for offerings), in which evaluation components can be changed and, ulimately, the course can be made available again. Another possibility is that a course can be **retired**, in which it is simply no longer available for creating course offerings.

## Events

Events describe what has *happened* in changes to the information in the domain.

* CourseAssigned
* PassMarkFixed
* EvaluationComponentsSet
* CourseMadeAvailable
* CourseReevaluated
* CourseRetired
* CourseScrapped

## Commands

Commands indicate some kind of *request* to change the information in the domain. Commands can fail (see Exceptions below).

* AssignCourse
* FixPassMark
* SetEvaluationComponents
* MakeCourseAvailable
* ReevaluateCourse
* RetireCourse
* ScrapCourse

## Exceptions

Exceptions describe when a command fails, indicating *why* it failed.

* CourseDuplication - for *AssignCourse*
* CourseNumberInvalid - for *AssignCourse*
* CourseNameInvalid - for *AssignCourse*
* CourseNotFound - for *FixPassMark*, *SetEvaluationComponents*, *MakeCourseAvailable*, *ReevaluateCourse*, *RetireCourse*, and *ScrapCourse*
* PassMarkIsInvalid - for *FixPassMark*
* InvalidEvaluationComponentWeight - for *SetEvaluationComponets*
* IncorrectTotalEvaluationComponentWeight - for *MakeCourseAvailable*
* CourseNotAvailable - for *ReevaluateCourse* and *RetireCourse*
* CoursePreviouslyReleased - for *ScrapCourse* (a course can be removed entirely from the list of courses if it was never made available, otherwise, as a point of historical record, it may have been used in a course offering, even if the offering never ran.)

> Question: Can you retire a course that is being re-evaluated? The answer is yes.

## Aggregates

The **Course** is an aggregate root, being composed of a *course number*, a *course name*, a *pass mark*, and a set of **evaluation components**. In turn, each **evaluation component** must have a *name* and a *relative weight*.

----


> In this domain, I am less concerned with events (what happened or what the intent was) than I am with state (having a stable/complete course evaluation and course). As such, I will *not* be using an Event Store, but rather an object state persistence. This means that as far as storage goes, I'm just doing CRUD in this context.

Central to the working of the system is setting up a set of **Evaluation Components** for a **Course**. First, a **Course** (identified by its course number and/or name) is "presumed" to exist in the system and is the aggregate root. For the **Course**, we may add an **Evaluation Component** (consisting primarily of a title and weight). **Evaluation Components** may or may not be controlled evaluations (a controlled evaluation is one that has some form of invigilation or supervision during the evaluation, whereas an uncontrolled evaluation is completed by the student on their own, without direct supervision). An **Evaluation Component** may be broken down into **SubComponents**. These **SubComponents** may be *weighted items or simply Pass/Fail items. Alternatively, the **SubComponents** may be a straight equal distribution of its parent evaluation weight. A **SubComponent** is the most granular level of marking recorded; a **SubComponent** cannot be broken down into smaller **SubComponents**.

Some of the rules around setting up **Courses** and their **Evaluation Components**:

* A **Courses** name and number must be unique (with the number often acting as the public identifier of a course).
* A **Courses** **Evaluation Components** cannot produce a total weight over 100. A **Course** can have its **Evaluation Components** weights total to less than 100.
* An **Evaluation Components** **SubComponents** must all be the same type: either individually weighted, equal distribution, or as Pass/Fail components.
* Individual **SubComponent** weights must add up to the weight of its main component
