using SimpleCqrs.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;
using SimpleCqrs;
using StudentMarks.Framework.Commands;
using SimpleCqrs.Commanding;

namespace StudentMarks.Framework.Specs
{
    [Story(AsA="", IWant="", SoThat="")]
    public class Draft_Queue
    {
        [Fact]
        public void NoName()
        {
            // Setup
            var runtime = new SampleRuntime();
            runtime.Start();

            // Arrange
            var command = new CreateCourseCommand("OOP-First Programming Fundamentals", "COMP1001", 50 );
            var commandBus = runtime.ServiceLocator.Resolve<ICommandBus>();

            // Act
            commandBus.Send(command);
            // Assert

            // Teardown
            runtime.Shutdown();
        }
    }
    public class SampleRuntime : SimpleCqrsRuntime<UnityServiceLocator>
    { }
}
