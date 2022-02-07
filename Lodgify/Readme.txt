Technologies used:
-------------------
.net 5 , EF Core 5


database design :
-------------------
	in order to respond to system requirements
	i decided to create these classes listed below and
	i used the code first approach with entity framework core , and I used the conventions of EF core (not fluent API) .
	The classes are :
		Person
		CookieOrder
		OrderDetails
		CookieType
		CookieTypePriceList


		person holds basic informations about persons or Employee

		CookieOrder holds informations about the order issued from a person 
		and it has a TotalAmount that holds the total of the cookies in the order, and it can contain multiple orders
		so i decided to create a class OrderDetails
		so there is a one to many relation ship between CookieOrder and OrderDetails

		CookieType holds informations about the cookie itself,and its current price

		CookieTypePriceList holds the history of the prices for a cookie at a certain date 
		in order to keep tracking of the prices of each cookie in a certain date.



Design Pattern of Code:
--------------------------
	i implemented the Repository pattern and unit of work pattern . they represent a layer of abstraction over the DBContext of EF Core
	so they are an abstraction between the  database and the controllers.
		the Repository Pattern : is implemented in a Generic way 
								by creating a IRepositoryBase interface
								and every entity or model has an interface that is inheriting the base interface.
								and the RepositoryImplementation holds classes 
											that implement the interfaces
											and inherting the RepositoryBase classe
		the Unit Of Work : IRepositoryStore and RpositoryStore let us aggregate all the CookiesInterface classes 
							and only register services.AddScoped<IRepositoryStore, RepositoryStore>(); in the 
							Dependancy Injection Container ,in Startup.cs in order to inject this service in the constructor 
							of each controller.
							in this way we minimized the services to be register in the dependancy injection container , and we can access 
							all the classess that are in IRepositoryStore.cs.




the End points: 
-------------------
I Used the Rest API to make the web api project

	A-REQUIRED END POINTS :
	Post/Order : i named it : https://localhost:44368/api/CookieOrders
					this will create a cookie order
					that is issued from a person or employee.
					One order can hold multiple types of cookies,
					each type of cookies has a quantity and a price.
					and when creating the order , I implemented a function that checks 
					if the budget has been reached or not(--the restriction point --)
					And finally each order is registered to a file as soon as it is received(--the persistence point --)

	Get/Order : i named it https://localhost:44368/api/CookieOrders  
				it retrieves all the orders

	PUT/CookieType : i named it : https://localhost:44368/api/cookieTypes/2
					using the classes CookieType and cookieTypePriceList
					 this end point updates the current price of a cookie and 
					 keeps tracking of the history of the prices at different dates.

	B-Additional end points:
		cookies end points :
		get https://localhost:44368/api/cookieTypes
		get https://localhost:44368/api/cookieTypes/3
		post https://localhost:44368/api/cookieTypes
		put https://localhost:44368/api/cookieTypes/6
		delete https://localhost:44368/api/cookieTypes/6   --EXCEPTION

		orders end points:
		get https://localhost:44368/api/CookieOrders
		get https://localhost:44368/api/CookieOrders/2
		post https://localhost:44368/api/CookieOrders
		put https://localhost:44368/api/CookieOrders/2
		delete https://localhost:44368/api/CookieOrders/2  --EXCEPTION

		get https://localhost:44368/api/CookieOrders/2/2022  --order per month and year
		get https://localhost:44368/api/CookieOrders/1/2/2022  --order per person and month and year
		get https://localhost:44368/api/CookieOrders/cookiType/2/2/2022 --orders per cookieType per month per year
		get https://localhost:44368/api/CookieTypes/cookiTypePrices/2 --consult the history of prices for a specific cookie type


		person end points: 
		get https://localhost:44368/api/People
		get https://localhost:44368/api/People/1
		post https://localhost:44368/api/People
		put https://localhost:44368/api/People/1
		delete https://localhost:44368/api/People/1


A way to call the API from CLI :
---------------------------------
	reference : https://docs.microsoft.com/en-us/aspnet/core/web-api/http-repl/?view=aspnetcore-5.0&tabs=windows
	the commands I used are : 
	* dotnet tool install -g Microsoft.dotnet-httprepl
		to install httprepl globally.
	* httprepl https://localhost:44368/api
	* ls and cd to see the endpoints
	* connect https://localhost:44368/api/cookieOrders
	* get

Another way to call the API from CLI:
--------------------------------------
using Curl : reference https://www.codepedia.org/ama/how-to-test-a-rest-api-from-command-line-with-curl/


issues:
--------

1-while implementing the end point : [HttpGet("cookiType/{id}/{month}/{year}")]
I used a nested foreach loop.
I am not satistfied with this method, but the linq query i used indicate a circular relationship , and does not work
even though i followed the conventions in implementing classes in EF core. 
so i could not resolve this problem 

2-problem with deleting cookieTypes and CookieOrder --EXCEPTION

3-TODO : updating TotalAmount in CookieOrder after updating orders in orderDetails and changing quantities

--------------------------
	
	personal comment:
	------------------
	I enjoyed a lot in doing this project 
	I dont have a working experience before in .Net 5 and web API











							

