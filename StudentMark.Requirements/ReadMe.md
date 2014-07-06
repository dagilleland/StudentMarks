# Student Marks - Narrative

As an instructor, I need to track marks for my students. For any given course, this entails a few tasks. Firstly, as part of initializing my tracking, I need to know/enter my class list (student first/last name + school id) and know/enter the different things I need to get marked (mark item, weight, etc.). After that, I need to just enter my marks, which I would like to do in a "bulk" fashion.

* **Q)** Can you describe further what you mean when you say you enter your marks in "bulk"?
  * **R)** Basically, after I mark an item like a quiz, I have all the quiz marks for the class, and I want to just pull up a list of the students and enter each student's quiz mark, and then just save the marks after I've entered them.
  * *Implementation Note* - When it comes to entering marks for students, it would be easiest to be able to click the marked item and then just enter each student's mark by using tab/alt-tab to go from student to student, and then bulk-add the class marks for the marked item.

In my current situation, I have a course with two quizzes (21% and 7%) and a set of all-or-nothing exercises (24 worth 3% each and totalling to 72%). Each quiz can be out of a differnt set of marks; for example, the first quiz is out of 40 marks. Since the exercises are worth all-or-nothing, they don't have individual marks.

When I enter my list of students, it would be easier to just paste in all the student information as one "chunk" and have the system figure out [parse] what the student information is. There would be one student per line, and all the information on each line is separated by spaces, tabs, and/or commas. Each line has the student's name (typically *First Last*, but perhaps with a middle name) and then ends with the school id for the student.

* [ ] **ToDo** - notes on how to enter the mark breakdown for the course

## User Stories

**User Stories** become the basis for System Tests and/or Integration Tests. *All the following user stories are from the perspective of the Instructor, since this is only an Instructor tool at present. Therefore, it can be assumed to start with* **An an Instructor***.*

* *Initialize Course Information*
  * **I Want** to enter the mark structure for the course,
  * **So That** I have the basic framework to enter student marks.
    * *Scenario Title* (Primary)
      * **Given** something **When** action **Then** result
    * *Scenario Title*
      * **Given** something **When** action **Then** result
* *Generate Class List*
  * **I Want** to enter a list of students in my class,
  * **So That** I know who to assign marks to.
    * *Scenario Title* (Primary)
      * **Given** something **When** action **Then** result
    * *Scenario Title*
      * **Given** something **When** action **Then** result
* *Title*
  * **I Want** to enter student marks,
  * **So That** I can track each student's progress in the course.
    * *Scenario Title* (Primary)
      * **Given** something **When** action **Then** result
    * *Scenario Title*
      * **Given** something **When** action **Then** result

----

* *Title*
  * **I Want** ,
  * **So That** .
    * *Scenario Title* (Primary)
      * **Given** something **When** action **Then** result
    * *Scenario Title*
      * **Given** something **When** action **Then** result

----

## Unit Tests

**Unit Tests** are for smaller parts of the system and are done as "units". I'm breaking these down into tests related to the *Application Framework* and those related to the *User Interface* (in this case, a browser). There will also be unit tests to check aspects of the "seams" between the UI and the App Framework.

### App Framework Unit Tests

* [ ] C# class library unit tests, done in xUnit.

### UI Unit Tests

* [ ] AngularJS functionality, tested with Jasmine.
  * Includes tests for the presence of WebAPI
