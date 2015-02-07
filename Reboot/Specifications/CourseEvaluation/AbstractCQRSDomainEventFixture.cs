using Edument.CQRS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StudentMarks.Framework.Specifications.CourseEvaluation
{
    public class AbstractCQRSDomainEventFixture<TDomain> where TDomain : Aggregate
    {
        protected void GivenPriorEvents(TDomain givenSut, object[] givenPriorEvents)
        {
            givenSut.ApplyEvents(givenPriorEvents);
        }
        protected void WhenDispatchingCommand<TCommand>(TDomain givenSut, TCommand whenCommand, out IEnumerable<object> actualEvents)
        {
            var handler = givenSut as IHandleCommand<TCommand>;
            if (handler == null)
                throw new CommandHandlerNotDefinedException(string.Format(
                    "Aggregate {0} does not yet handle command {1}",
                    givenSut.GetType().Name, whenCommand.GetType().Name));
            IEnumerable<object> actual = null;

            // Expected events, but got exception {0}
            Assert.DoesNotThrow(() =>
            {
                actual = handler.Handle(whenCommand).Cast<object>();
            });

            actualEvents = actual;
        }
        protected void ThenExpectedEventsAreGenerated(IEnumerable<object> expectedEvents, IEnumerable<object> gotEvents)
        {
            if (gotEvents != null)
            {
                if (gotEvents.Count() < expectedEvents.Count())
                    Assert.True(false, string.Format("Expected event(s) missing: {0}",
                        string.Join(", ", EventDiff(expectedEvents.ToArray(), gotEvents.ToArray()))));
                else if (gotEvents.Count() > expectedEvents.Count())
                    Assert.True(false, string.Format("Unexpected event(s) emitted: {0}",
                        string.Join(", ", EventDiff(gotEvents.ToArray(), expectedEvents.ToArray()))));
                else
                {
                    var expectedAndActual = expectedEvents.Zip(gotEvents, (e, a) => new { Expected = e, Actual = a });
                    foreach (var pair in expectedAndActual)
                    {
                        if (pair.Actual.GetType() == pair.Expected.GetType())
                        {
                            if (pair.Actual.GetType().IsSerializable)
                            {
                                string serializeExpected = Serialize(pair.Expected);
                                string serializeActual = Serialize(pair.Actual);
                                Assert.Equal(serializeExpected, serializeActual);
                            }
                            else
                                Assert.True(false, string.Format("Event {0} is not serializable", pair.Actual.GetType().Name));
                        }
                        else
                            Assert.True(false, string.Format(
                                "Incorrect event in results; expected a {0} but got a {1}",
                                pair.Expected.GetType().Name, pair.Actual.GetType().Name));
                    }
                }
                //for (var i = 0; i < gotEvents.Count(); i++)
                //    if (gotEvents[i].GetType() == expectedEvents[i].GetType())
                //        Assert.Equal(Serialize(expectedEvents[i]), Serialize(gotEvents[i]));
                //    else
                //        Assert.True(false, string.Format(
                //            "Incorrect event in results; expected a {0} but got a {1}",
                //            expectedEvents[i].GetType().Name, gotEvents[i].GetType().Name));
            }
            //else if (got is CommandHandlerNotDefinedException)
            //    Assert.True(false, (got as Exception).Message);
            //else
            //    Assert.True(false, string.Format(,
            //        got.GetType().Name));
        }

        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private string[] EventDiff(object[] a, object[] b)
        {
            var diff = a.Select(e => e.GetType().Name).ToList();
            foreach (var remove in b.Select(e => e.GetType().Name))
                diff.Remove(remove);
            return diff.ToArray();
        }

        private class CommandHandlerNotDefinedException : Exception
        {
            public CommandHandlerNotDefinedException(string msg) : base(msg) { }
        }
    }
}
