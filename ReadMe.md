# StudentMarks - An Advancing Productivity Project



----
## Lessons Learned

* Entity Framework
  * During the early development phases, when the model is changing, it makes sense to use the `DropCreateDatabaseIfModelChanges` database initializer, which can be configured in the database as shown in *code Snippet A*
    * The only caveate is that when using `[Autorollback]` along with `[Theory]` and multiple `[InlineData(...)]` in the unit/acceptance tests, you can sometimes get "bogus" failures stating that the new table (entity) does not exist in the database (*Invalid object name dbo.NewTable*). In those cases, you might see one `InlineData(...)` pass while all the others fail. The reason for this is that the Autorollback of the passing one also rolls back the creation of the table.
      * **Quick Fix:** Simply remove the `[Autorollback]` during the first run of the test with the new/changed table, then "clean up" you database from the bogus data, and re-add `[Autorollback]`. Do this each time the model changes.
      * **Long-Term Fix:** A nicer, long-term fix is to create an `IUseFixture<T>` on the test class along with a `SetInitializer()` for the context. This also leads you to build *Dependency Injection* into your solution and leverage it for your testing. This can benefit both Acceptance Tests and Unit Tests. See *Code Snippet B*.

----
### Code Snippets

#### A - App.Config use of `DropCreateDatabaseIfModelChanges`.

```xml
    <contexts>
      <context type="StudentMarks.App.DAL.AppContext, StudentMarks">
        <databaseInitializer type="System.Data.Entity.DropCreateDatabaseIfModelChanges`1[[StudentMarks.App.DAL.AppContext, StudentMarks]], EntityFramework" />
      </context>
    </contexts>
```

#### B - Sample use of `IUseFixture<T>` with `SetInitializer(...)` on a `DbContext`

```csharp
    internal class SeededDbContext : MyAppContext, IDisposable
    {
        public Exception InitializationException { get; set; }
        public SeededDbContext()
        {
            Database.SetInitializer<SeededDbContext>(new ForceDeleteInitializer());
        }
    }
    // from http://stackoverflow.com/a/5289296/2154662
    internal class ForceDeleteInitializer : IDatabaseInitializer<SeededDbContext>
    {
        private readonly IDatabaseInitializer<SeededDbContext> _initializer;

        public ForceDeleteInitializer()
        {
            _initializer = new MyAppModelInitializer<SeededDbContext>();
        }

        public void InitializeDatabase(SeededDbContext context)
        {
            try
            {
                // NOTE: http://www.danbartram.com/entity-framework-6-and-executesqlcommand/
                string sqlCommand = string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database);
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sqlCommand);
                _initializer.InitializeDatabase(context);
            }
            catch (Exception ex)
            {
                context.InitializationException = ex;
            }
        }
    }
    public class When_Initializing_Database_With_Seed_Data : IUseFixture<SeededDbContext>
    {
        #region IUseFixture
        private MyAppContext SUT = null;

        private List<DataInitialization> ActualPrePopulateDate;
        private List<Product> ActualInventories;

        private readonly string[] UnTrackedNaitCodes = { "NAIT-123", "NAIT-125", "NAIT-126" };

        void IUseFixture<SeededDbContext>.SetFixture(SeededDbContext data)
        {
            SUT = data;
            if (data.InitializationException == null)
            {
                // 
                ActualPrePopulateDate = SUT.DataInitializations.ToList();
                ActualInventories = SUT.Inventories.ToList();
            }
        }
        #endregion

        #region Should DropCreate Database
        [Fact]
        public void Should_Always_Drop_Create_Database()
        {
            var actual = SUT as SeededDbContext;
            
            //Assert.NotNull(actual);
            Exception core = actual.InitializationException;
            if (core != null)
            {
                while(core.InnerException != null)
                    core = core.InnerException;
                Assert.Equal<string>(null, core.Message);
            }
            Assert.Null(core); // meh - probably won't get thrown, but included anyway
        }
        #endregion

        // ... more tests
    }
```
