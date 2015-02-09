using Edument.CQRS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Edument.CQRS.Specs.Tests
{
    public class AbstractCQRSDomainEventFixture<TDomain> where TDomain : Aggregate, new()
    {
        #region Constructor/Properties - Setup
        protected TDomain SUT_ActualDomain { get; private set; }
        public AbstractCQRSDomainEventFixture()
        {
            SUT_ActualDomain = new TDomain();
        }
        #endregion

        #region Common Given/When/Then Methods
        protected void GivenPriorEvents(Guid aggregateId, object[] givenPriorEvents)
        {
            SUT_ActualDomain.ApplyEvents(givenPriorEvents);
        }

        protected void WhenDispatchingCommand<TCommand>(TCommand whenCommand, out IEnumerable<object> actualEvents)
        {
            IHandleCommand<TCommand> handler = GetHandler<TCommand>();

            // Expected events, but got exception {0}
            IEnumerable<object> actual = null;
            Assert.DoesNotThrow(() =>
            {
                actual = handler.Handle(whenCommand).Cast<object>();
            });

            actualEvents = actual;
        }

        protected void FailWhenDispatchingCommand<TCommand, TException>(TCommand whenCommand, out TException actualException) where TException : Exception
        {
            IHandleCommand<TCommand> handler = GetHandler<TCommand>();

            actualException = Assert.Throws<TException>(() =>
            {
                // Note: In case the Handle just yields an enumerator, I must invoke the enumerator to ensure any internal validation checks actually pass. See http://jason.diamond.name/weblog/2010/10/27/why-wont-my-iterator-throw/
                var result = handler.Handle(whenCommand);
                result.GetEnumerator().MoveNext(); // The "guard" is done by MoveNext()
            });
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
            }
        }
        #endregion

        #region Private helper methods
        private IHandleCommand<TCommand> GetHandler<TCommand>()
        {
            var handler = SUT_ActualDomain as IHandleCommand<TCommand>;
            if (handler == null)
                throw new CommandHandlerNotDefinedException(string.Format(
                    "Aggregate {0} does not yet handle command {1}",
                    SUT_ActualDomain.GetType().Name, typeof(TCommand).Name));
            return handler;
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
        #endregion

        #region Exceptions
        private class CommandHandlerNotDefinedException : Exception
        {
            public CommandHandlerNotDefinedException(string msg) : base(msg) { }
        }
        #endregion
    }
}
