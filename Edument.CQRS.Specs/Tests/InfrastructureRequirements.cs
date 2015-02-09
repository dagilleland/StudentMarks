using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edument.CQRS.Specs.Tests
{
    /* Objective: DIST - Domain/Infrastructure Segregation Tests

     *
     *  Imagine the idea of "Unobtrusive CQRS and Event Sourcing".
     *
     *  Domain Driven Design (DDD) and Behaviour Driven Development (BDD) are two highly complementary approaches to software development. Both of them offer techniques and practices that can shape an application into exactly "what the customer ordered" by illuminating the "nature" of the application, that is, its Domain. But taking this understanding of the Domain and breathing life into it by making a working application is no small task. A software application is much more than just the Domain - there's the whole question of the infrastructure that supports the Domain.
     *
     * In performing DDD and BDD, software developers have to tackle questions around connecting the Domain
     *  to the "outside" world, and these questions often revolve around two primary aspects:
     *      - The "front-end" of the application, which is often the primary source of input and ultimate target of output.
     *      - The "back-end" of the application, which must be concerned with persisting information critical to the domain.
     *
     *  For the back-end, the main persistence choices are to do state-based persistence (as in most CRUD systems) or event-based persistence (also known as Event Sourcing).
     *  On the front end, BDD may reveal that the Domain (with the help of a bit of DTOs) is mostly sufficient for both input and output. Alternatively, a BDD analysis might reveal that CQRS would be more appropriate.
     *  
     *  Resolving the questions of what to use in the back-end and the front-end bring with it implications for the infrastructure of the application as a whole, and it is not unusual to see the design of the Domain as directly affected by those decisions. As these infrastructure aspects become more intertwined with the Domain itself, it's also not uncommon to see the whole design of the system slowly devolve into a "big ball of mud".
     *

     *  Refine the Edument CQRS infrastructure to achieve the following:
     *  - Establish testing patterns that make us of both
     *      - In-Memory Event Stores & Repositories
     *      - SQL Event Stores & Repositories
     *      - Base class for common BDD style testing of
     *          - Commands (which should produce a) Events and b) changes to the domain)
     *          - Queries (which should re-assemble information from Events)
     *  - Refine the domain aggregate base class to
     *  - Improve Command and Event routing (Bus)
     *      - The Edument MessageDispatcher sets the domain aggregate's Id directly, which strikes me as over-trusting and over-presumptuous - overtrusting on the part of the domain aggregate base class, and too presumptuous on the part of the MessageDispatcher
     */
    public class InfrastructureRequirements
    {
    }
}
