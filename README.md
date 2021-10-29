# CDRBIP
Call Detail Record Business Intelligence Platform API

# What choices have I made?
I decided to take a Clean Architecture approach for this API, implementing simple application/domain/infrastructure layers to reflect this.

The flow of control is always inwards, in the future this will allow us to split our layers into seperate APIs (if need be) to support a microservices approach.

You will see that I have included all CallDetailRecord implementation within a module folder. The reason I have done this is to seperate our domains clearly. The addition of additional modules to the application should be painless and straight-forward to support extensability of the API. It may even be the case that we may want to provide each module with it's own IoC container, to allow for singleton instances of logger and our quartz scheduler for each module.

I have opted to use Mediator here to keep the controller logic clean consise, thanks to this our controllers are extremely simple with only 1 or 2 lines of code required. This also keeps our application business logic seperate from our controllers and indipendant from eachother.

All Mediatr handlers sit alongside their query in the same class. This is an uncommon approach to the CQRS pattern and usually you find that your Queries/Commands are sitting in seperate folders from their handlers. As a matter of preference and ease of reading I have opted to keep them within the same class, at a glance it is easy to understand/change either query/command and their handler.

You will see that each handler is also quite simple, and is our first use of the repository pattern. Each handler has the appropraite repository injected and then a call is made to whatever method is required within that repository, passing the request details. This has allowed us to keep our data access logic seperate from our Mediatr handlers, meaning that any changes to ORM would only need to occur in one place, our repository.

The implementation of the repository pattern also enables us to have something to mock for our unit testing framework, that way we can test our Mediatr handlers indipendant of the ORM, in this case EntityFramework Core.

You will also see that I have opted to use an in-memory DB for this task. The reason for this is both time constraints and simplicity of our database, being only a single table. I wholly expect this to change in the future to make use of a SqlServer database.

Finally, I have opted to use the scheduling framework Quartz.net. This is to allow all CDR csv files to be uploaded and stored in the database elegently. No calls are needing to be made to the API, source systems simply upload their CSV to a specific file location and our API will pick those files up and handle them, also creating a 'bad records' file containing any files we werent able to load into the database for whatever reason.

# What assumptions have I made?
I have assumed that files will be uploaded by a user onto either an SFTP or AWS S3 bucket. Although the implementation of the AWS sdk has not been done in this technical task, it is an option for future enhancements.

I have also assumed that users will have their own process to retrieve 'bad records' files and clear them from the file location. 

# How to run the application locally
You can download the source code from this repository. If you have Visual Studio you can simply run this API locally using Visual Studio via IIS Express.

If you do not have or want to use Visual Studio you can run this API using command line, ensure you are in the same directory as the CDRBIP project and run:
> dotnet watch run (for this to work you require the dotnet SDK.)

Once the application is up and running locally you should now replicate the file upload process that users will go through.

Take your sample csv file (ensure it is named as expected "techtest_cdr_dataset.csv" and paste it to the following location: 
\CDRBIP\SampleCsv

In a couple of seconds you should see that your .csv file will be replaced with a file named "techtest_cdr_dataset_bad_records.csv". This means that the file has been stored in our database and the files in this new .csv are all the records we couldnt upload (either they had duplicate references or had bad data).

A good way to test that this all worked as you expect is to make some calls to the API endpoints. Please use the following payloads:

	Retrieve individual CDR by the CDR Reference:
		https://localhost:{port}/api/calldetailrecord/GetByReference/?reference={reference}

	Retrieve a count and total duration of all calls in a specified time period
		https://localhost:{port}/api/calldetailrecord/GetCallCountAndDuration/?callType={1/2}

	Retrieve all CDRs for a specific Caller ID in a specified time period.
		https://localhost:{port}/api/calldetailrecord/GetByCallerId/?callerId={callerId}

	Retrieve N most expensive calls, in GBP, for a specific Caller ID
		https://localhost:{port}/api/calldetailrecord/GetMostExpensiveCalls/?callerId={callerid}&requestedAmount={requestedamount}&callType={1/2}


Where you see {X} replace the brackets and their contents with your data.

You can either use postman or simply paste these into your browser. You should see the return return JSON object.

# Future Enhancements
There are a number of ways this application can be enhanced. Ill outline those below;

Firstly, we need to implement a relational database, be that Postgres/SqlServer etc.. This will enable us to start creating a more complex API and for our data to be persistant between app startup and teardown. We will use either Entity Framework migrations or other tools such as Roundhouse or DbUp to execute our SQL against our database instance. We will then need to change our DbContext slightly as we are no longer using InMemory, however due to the isolated nature of this code, it will not have large impacts. This will enable us to make use of a more extensive integration test framework.

We then need to take a different approach to the csv file download process. Currently, we are only looking for the .csv locally however this will of course not work in any real world scenarios. We could make use of an integration with AWS S3 buckets using the AWS sdk in our API. A user could simply upload their files to bucket and our API could pick files up from that location instead of locally. Alternatively, if AWS is out of the question we could simply retrieve the file from a shared SFTP directory. 

Once we start considering this application for deployments we need to implement some additional appsettins.json files (local / development / production etc..). We can then make use of pipeline variables to ensure out appsettings values are kept secret such as passwords in connection strings.




