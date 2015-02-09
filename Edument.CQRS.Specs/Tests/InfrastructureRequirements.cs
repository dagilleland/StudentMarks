using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edument.CQRS.Specs.Tests
{
    /* Objective: DIST - Domain/Infrastructure Segregation Tests
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
